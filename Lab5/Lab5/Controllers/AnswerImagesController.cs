using Azure;
using Azure.Storage.Blobs;
using Lab5.Data;
using Lab5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class AnswerImagesController : Controller
    {
        private readonly AnswerImageDataContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
        private readonly int _fileSizeLimit = 500000;

        public AnswerImagesController(AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AnswerImages.ToListAsync());
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile answerFile, int imageType)
        {

            // Check to make sure file was selected
            if (answerFile == null)
            {
                return View("Error");
            }

            var length = answerFile.Length;
            BlobContainerClient containerClient;
            string containerName = "";
            string filename = "";
            string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };

            // Check file size
            if(answerFile.Length > _fileSizeLimit)
            {
                return View("Error");
            }

            // get containername
            if (imageType == 0) { containerName = earthContainerName; }
            else { containerName = computerContainerName; }

            // get new filename
            string ext = Path.GetExtension(answerFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return View("Error");
            }
            else
            {
                filename = Path.GetRandomFileName();
            }

            // Get container to hold the blob
            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            // create and store blob and answerImage
            try
            {
                var blockBlob = containerClient.GetBlobClient(filename);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using(var memoryStream = new MemoryStream())
                {
                    await answerFile.CopyToAsync(memoryStream);

                    memoryStream.Position = 0;

                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                var image = new AnswerImage();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = answerFile.FileName;
                image.QuestionId = imageType;

                _context.AnswerImages.Add(image);
                _context.SaveChanges();
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.AnswerImages.FirstOrDefaultAsync(m => m.AnswerImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.AnswerImages.FindAsync(id);
            string containerName = "";

            if(image.Question == Question.Earth) { containerName = earthContainerName; }
            else { containerName = computerContainerName; }

            BlobContainerClient containerClient;
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            try
            {
                // Get the blob that holds the data
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.AnswerImages.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
    }
}

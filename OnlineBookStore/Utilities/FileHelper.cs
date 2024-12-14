using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OnlineBookStore.Utilities
{

    public static class FileHelper
    {
        private static readonly string _imagesFolder;

        static FileHelper()
        {
            // Initialize the images directory path
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(currentDir)?.Parent?.Parent?.Parent?.FullName;
            _imagesFolder = Path.Combine(projectRoot, "media", "images");

            // Ensure the directory exists
            if (!Directory.Exists(_imagesFolder))
            {
                Directory.CreateDirectory(_imagesFolder);
            }
        }

        /// <summary>
        /// Saves an image to the predefined images folder.
        /// </summary>
        /// <param name="imageBytes">The image data as a byte array.</param>
        /// <param name="fileExtension">Optional file extension. Default is .jpg.</param>
        /// <returns>The full path to the saved image file.</returns>
        public static string SaveImage(byte[] imageBytes, string fileExtension = ".jpg")
        {
            if (imageBytes == null || imageBytes.Length == 0)
                throw new ArgumentException("Image bytes cannot be null or empty.");

            if (string.IsNullOrEmpty(fileExtension) || !fileExtension.StartsWith("."))
                throw new ArgumentException("Invalid file extension.");

            try
            {
                string fileName = $"Image_{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(_imagesFolder, fileName);

                File.WriteAllBytes(filePath, imageBytes);

                return filePath; // Return the full path of the saved image
            }
            catch (Exception ex)
            {
                throw new IOException($"Error saving image: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Loads an image from the file system.
        /// </summary>
        /// <param name="filePath">The full path to the image file.</param>
        /// <returns>The image data as a byte array.</returns>
        public static byte[] LoadImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Image file not found at: {filePath}");
            }
            catch (Exception ex)
            {
                throw new IOException($"Error loading image: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes an image from the file system.
        /// </summary>
        /// <param name="filePath">The full path to the image file.</param>
        public static void DeleteImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Error deleting image: {ex.Message}", ex);
            }
        }
    }
}

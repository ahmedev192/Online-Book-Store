using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OnlineBookStore.Utilities
{

    public class FileHelper
    {
        public static string SaveImage(byte[] imageBytes)
        {
            try
            {
                // Get the current directory and navigate to the project root
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Directory.GetParent(currentDir)?.Parent?.Parent?.Parent?.FullName;

                // Define the media/images directory
                string imagesFolder = Path.Combine(projectRoot, "media", "images");

                // Ensure the directory exists
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                // Generate a unique file name
                string fileName = $"Image_{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(imagesFolder, fileName);

                // Save the image bytes to the file
                File.WriteAllBytes(filePath, imageBytes);

                return filePath; // Return the relative or full path of the saved image
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image: {ex.Message}");
                return null;
            }
        }

        public static byte[] LoadImage(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
                return null;
            }
        }
    }

}

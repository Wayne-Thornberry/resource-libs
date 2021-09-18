using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class Resources
    {
        /// <summary>
        /// Loads a file from a resource given the resource name and filename.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string LoadFile(string resourceName, string fileName)
        {
            // Check if the resource exists
            // if the file exists in the cache, retrive it there, otherwise
            // Load the file from the resource
            // Cache then file data temperarly to prevent calling the same api twice
            return "";
        }

        /// <summary>
        /// Loads a file contents either by looking at the resources loaded and its files
        /// </summary> 
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string LoadFile(string fileName)
        {
            // Check if the resource exists
            // if the file exists in the cache, retrive it there, otherwise
            // Load the file from the resource
            // Cache then file data temperarly to prevent calling the same api twice
            return "";
        }

        /// <summary>
        /// Returns all the resource names loaded
        /// </summary>
        /// <returns></returns>
        public static string[] GetResourceNames()
        {
            return null;
        }
    }
}

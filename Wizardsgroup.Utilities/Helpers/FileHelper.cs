using System;
using System.Configuration;
using System.IO;

namespace Wizardsgroup.Utilities.Helpers
{
    public static class FileHelper
    {

        private static readonly string tempPath = ConfigurationManager.AppSettings["TempPath"];
        private static readonly string docPath = ConfigurationManager.AppSettings["DocPath"];
        private static readonly string templatePath = ConfigurationManager.AppSettings["TemplatePath"];

        public static byte[] GetFile(string filename)
        {
            FileStream fs = File.OpenRead(filename);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);

            if (br != fs.Length)
                throw new IOException(filename);

            return data;
        }

        public static string GetContentType(string fileExtension)
        {
            string contentType = "text/html";
            string extension = fileExtension;

            if (extension.Contains("."))
                extension = extension.Replace(".", "");

            switch (extension)
            {
                case "ai":
                    contentType = "application/postscript"; break;

                case "aif":
                    contentType = "audio/x-aiff"; break;

                case "aifc":
                    contentType = "audio/x-aiff"; break;

                case "aiff":
                    contentType = "audio/x-aiff"; break;

                case "asc":
                    contentType = "text/plain"; break;

                case "atom":
                    contentType = "application/atom+xml"; break;

                case "au":
                    contentType = "audio/basic"; break;

                case "avi":
                    contentType = "video/x-msvideo"; break;

                case "bcpio":
                    contentType = "application/x-bcpio"; break;

                case "bin":
                    contentType = "application/octet-stream"; break;

                case "bmp":
                    contentType = "image/bmp"; break;

                case "cdf":
                    contentType = "application/x-netcdf"; break;

                case "cgm":
                    contentType = "image/cgm"; break;

                case "class":
                    contentType = "application/octet-stream"; break;

                case "cpio":
                    contentType = "application/x-cpio"; break;

                case "cpt":
                    contentType = "application/mac-compactpro"; break;

                case "csh":
                    contentType = "application/x-csh"; break;

                case "css":
                    contentType = "text/css"; break;

                case "dcr":
                    contentType = "application/x-director"; break;

                case "dif":
                    contentType = "video/x-dv"; break;

                case "dir":
                    contentType = "application/x-director"; break;

                case "djv":
                    contentType = "image/vnd.djvu"; break;

                case "djvu":
                    contentType = "image/vnd.djvu"; break;

                case "dll":
                    contentType = "application/octet-stream"; break;

                case "dmg":
                    contentType = "application/octet-stream"; break;

                case "dms":
                    contentType = "application/octet-stream"; break;

                case "doc":
                    contentType = "application/msword"; break;

                case "dtd":
                    contentType = "application/xml-dtd"; break;

                case "dv":
                    contentType = "video/x-dv"; break;

                case "dvi":
                    contentType = "application/x-dvi"; break;

                case "dxr":
                    contentType = "application/x-director"; break;

                case "eps":
                    contentType = "application/postscript"; break;

                case "etx":
                    contentType = "text/x-setext"; break;

                case "exe":
                    contentType = "application/octet-stream"; break;

                case "ez":
                    contentType = "application/andrew-inset"; break;

                case "gif":
                    contentType = "image/gif"; break;

                case "gram":
                    contentType = "application/srgs"; break;

                case "grxml":
                    contentType = "application/srgs+xml"; break;

                case "gtar":
                    contentType = "application/x-gtar"; break;

                case "hdf":
                    contentType = "application/x-hdf"; break;

                case "hqx":
                    contentType = "application/mac-binhex40"; break;

                case "htm":
                    contentType = "text/html"; break;

                case "html":
                    contentType = "text/html"; break;

                case "ice":
                    contentType = "x-conference/x-cooltalk"; break;

                case "ico":
                    contentType = "image/x-icon"; break;

                case "ics":
                    contentType = "text/calendar"; break;

                case "ief":
                    contentType = "image/ief"; break;

                case "ifb":
                    contentType = "text/calendar"; break;

                case "iges":
                    contentType = "model/iges"; break;

                case "igs":
                    contentType = "model/iges"; break;

                case "jnlp":
                    contentType = "application/x-java-jnlp-file"; break;

                case "jp2":
                    contentType = "image/jp2"; break;

                case "jpe":
                    contentType = "image/jpeg"; break;

                case "jpeg":
                    contentType = "image/jpeg"; break;

                case "jpg":
                    contentType = "image/jpeg"; break;

                case "js":
                    contentType = "application/x-javascript"; break;

                case "kar":
                    contentType = "audio/midi"; break;

                case "latex":
                    contentType = "application/x-latex"; break;

                case "lha":
                    contentType = "application/octet-stream"; break;

                case "lzh":
                    contentType = "application/octet-stream"; break;

                case "m3u":
                    contentType = "audio/x-mpegurl"; break;

                case "m4a":
                    contentType = "audio/mp4a-latm"; break;

                case "m4b":
                    contentType = "audio/mp4a-latm"; break;

                case "m4p":
                    contentType = "audio/mp4a-latm"; break;

                case "m4u":
                    contentType = "video/vnd.mpegurl"; break;

                case "m4v":
                    contentType = "video/x-m4v"; break;

                case "mac":
                    contentType = "image/x-macpaint"; break;

                case "man":
                    contentType = "application/x-troff-man"; break;

                case "mathml":
                    contentType = "application/mathml+xml"; break;

                case "me":
                    contentType = "application/x-troff-me"; break;

                case "mesh":
                    contentType = "model/mesh"; break;

                case "mid":
                    contentType = "audio/midi"; break;

                case "midi":
                    contentType = "audio/midi"; break;

                case "mif":
                    contentType = "application/vnd.mif"; break;

                case "mov":
                    contentType = "video/quicktime"; break;

                case "movie":
                    contentType = "video/x-sgi-movie"; break;

                case "mp2":
                    contentType = "audio/mpeg"; break;

                case "mp3":
                    contentType = "audio/mpeg"; break;

                case "mp4":
                    contentType = "video/mp4"; break;

                case "mpe":
                    contentType = "video/mpeg"; break;

                case "mpeg":
                    contentType = "video/mpeg"; break;

                case "mpg":
                    contentType = "video/mpeg"; break;

                case "mpga":
                    contentType = "audio/mpeg"; break;

                case "ms":
                    contentType = "application/x-troff-ms"; break;

                case "msh":
                    contentType = "model/mesh"; break;

                case "mxu":
                    contentType = "video/vnd.mpegurl"; break;

                case "nc":
                    contentType = "application/x-netcdf"; break;

                case "oda":
                    contentType = "application/oda"; break;

                case "ogg":
                    contentType = "application/ogg"; break;

                case "pbm":
                    contentType = "image/x-portable-bitmap"; break;

                case "pct":
                    contentType = "image/pict"; break;

                case "pdb":
                    contentType = "chemical/x-pdb"; break;

                case "pdf":
                    contentType = "application/pdf"; break;

                case "pgm":
                    contentType = "image/x-portable-graymap"; break;

                case "pgn":
                    contentType = "application/x-chess-pgn"; break;

                case "pic":
                    contentType = "image/pict"; break;

                case "pict":
                    contentType = "image/pict"; break;

                case "png":
                    contentType = "image/png"; break;

                case "pnm":
                    contentType = "image/x-portable-anymap"; break;

                case "pnt":
                    contentType = "image/x-macpaint"; break;

                case "pntg":
                    contentType = "image/x-macpaint"; break;

                case "ppm":
                    contentType = "image/x-portable-pixmap"; break;

                case "ppt":
                    contentType = "application/vnd.ms-powerpoint"; break;

                case "ps":
                    contentType = "application/postscript"; break;

                case "qt":
                    contentType = "video/quicktime"; break;

                case "qti":
                    contentType = "image/x-quicktime"; break;

                case "qtif":
                    contentType = "image/x-quicktime"; break;

                case "ra":
                    contentType = "audio/x-pn-realaudio"; break;

                case "ram":
                    contentType = "audio/x-pn-realaudio"; break;

                case "ras":
                    contentType = "image/x-cmu-raster"; break;

                case "rdf":
                    contentType = "application/rdf+xml"; break;

                case "rgb":
                    contentType = "image/x-rgb"; break;

                case "rm":
                    contentType = "application/vnd.rn-realmedia"; break;

                case "roff":
                    contentType = "application/x-troff"; break;

                case "rtf":
                    contentType = "text/rtf"; break;

                case "rtx":
                    contentType = "text/richtext"; break;

                case "sgm":
                    contentType = "text/sgml"; break;

                case "sgml":
                    contentType = "text/sgml"; break;

                case "sh":
                    contentType = "application/x-sh"; break;

                case "shar":
                    contentType = "application/x-shar"; break;

                case "silo":
                    contentType = "model/mesh"; break;

                case "sit":
                    contentType = "application/x-stuffit"; break;

                case "skd":
                    contentType = "application/x-koan"; break;

                case "skm":
                    contentType = "application/x-koan"; break;

                case "skp":
                    contentType = "application/x-koan"; break;

                case "skt":
                    contentType = "application/x-koan"; break;

                case "smi":
                    contentType = "application/smil"; break;

                case "smil":
                    contentType = "application/smil"; break;

                case "snd":
                    contentType = "audio/basic"; break;

                case "so":
                    contentType = "application/octet-stream"; break;

                case "spl":
                    contentType = "application/x-futuresplash"; break;

                case "src":
                    contentType = "application/x-wais-source"; break;

                case "sv4cpio":
                    contentType = "application/x-sv4cpio"; break;

                case "sv4crc":
                    contentType = "application/x-sv4crc"; break;

                case "svg":
                    contentType = "image/svg+xml"; break;

                case "swf":
                    contentType = "application/x-shockwave-flash"; break;

                case "t":
                    contentType = "application/x-troff"; break;

                case "tar":
                    contentType = "application/x-tar"; break;

                case "tcl":
                    contentType = "application/x-tcl"; break;

                case "tex":
                    contentType = "application/x-tex"; break;

                case "texi":
                    contentType = "application/x-texinfo"; break;

                case "texinfo":
                    contentType = "application/x-texinfo"; break;

                case "tif":
                    contentType = "image/tiff"; break;

                case "tiff":
                    contentType = "image/tiff"; break;

                case "tr":
                    contentType = "application/x-troff"; break;

                case "tsv":
                    contentType = "text/tab-separated-values"; break;

                case "txt":
                    contentType = "text/plain"; break;

                case "ustar":
                    contentType = "application/x-ustar"; break;

                case "vcd":
                    contentType = "application/x-cdlink"; break;

                case "vrml":
                    contentType = "model/vrml"; break;

                case "vxml":
                    contentType = "application/voicexml+xml"; break;

                case "wav":
                    contentType = "audio/x-wav"; break;

                case "wbmp":
                    contentType = "image/vnd.wap.wbmp"; break;

                case "wbmxl":
                    contentType = "application/vnd.wap.wbxml"; break;

                case "wml":
                    contentType = "text/vnd.wap.wml"; break;

                case "wmlc":
                    contentType = "application/vnd.wap.wmlc"; break;

                case "wmls":
                    contentType = "text/vnd.wap.wmlscript"; break;

                case "wmlsc":
                    contentType = "application/vnd.wap.wmlscriptc"; break;

                case "wrl":
                    contentType = "model/vrml"; break;

                case "xbm":
                    contentType = "image/x-xbitmap"; break;

                case "xht":
                    contentType = "application/xhtml+xml"; break;

                case "xhtml":
                    contentType = "application/xhtml+xml"; break;

                case "xls":
                    contentType = "application/vnd.ms-excel"; break;

                case "xml":
                    contentType = "application/xml"; break;

                case "xpm":
                    contentType = "image/x-xpixmap"; break;

                case "xsl":
                    contentType = "application/xml"; break;

                case "xslt":
                    contentType = "application/xslt+xml"; break;

                case "xul":
                    contentType = "application/vnd.mozilla.xul+xml"; break;

                case "xwd":
                    contentType = "image/x-xwindowdump"; break;

                case "xyz":
                    contentType = "chemical/x-xyz"; break;

                case "zip":
                    contentType = "application/zip"; break;
            }

            return contentType;
        }

        public static void DeleteFiles(string path)
        {
            DeleteFiles(path, new TimeSpan(0, 0, 0));
        }

        public static void DeleteFiles(string path, TimeSpan olderThan)
        {
            DeleteFiles(path, olderThan, string.Empty);
        }

        public static void DeleteFiles(string path, TimeSpan olderThan, string searchPattern)
        {
            // delete temp files older than a certain period
            var files = new DirectoryInfo(path)
                .GetFiles();

            if (searchPattern != string.Empty)
                files = new DirectoryInfo(path)
                    .GetFiles(searchPattern);

            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.Subtract(olderThan))
                {
                    try
                    {
                        System.IO.File.Delete(file.FullName);
                    }
                    catch (IOException ex)
                    {
                        Logger.Log(ex.ToString());
                    }
                }
            }
        }

        public static string ServerTempPath(Func<string, string> mapPath, string filepath)
        {
            if (filepath == null)
                return mapPath(tempPath);
            return Path.Combine(mapPath(tempPath), filepath);
        }

        public static string ServerDocPath(Func<string, string> mapPath, string filepath)
        {
            if (filepath == null)
                return mapPath(docPath);
            return Path.Combine(mapPath(docPath), filepath);
        }

        public static string RemoveDatePrefixFilename(string filename)
        {
            return filename!= null ? filename.Substring(filename.IndexOf('_') + 1) : string.Empty;
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetFilename(string filename)
        {
            try
            {
                return filename.Substring(filename.IndexOf('_') + 1);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static byte[] ConvertToBytes(string sourcePath)
        {
            byte[] file = null;
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            return file;
        }

        public static void CopyTemplateToTempFolder(Func<string, string> mapPath, string templateFilename, string destinationFilename)
        {
            File.Copy(Path.Combine(mapPath(templatePath), templateFilename), Path.Combine(mapPath(tempPath), destinationFilename));
        }
    }
}

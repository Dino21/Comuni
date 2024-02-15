using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Linq;
using System.Text;

namespace INI
{
    public class IniFile
    {
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
        static extern int GetSectionNamesListA(byte[] lpszReturnBuffer, int nSize, string lpFileName);
        /// <summary>
        /// Get list<string> of the Section list of the ini File (theFile)
        /// </summary>
        /// <returns></returns>
        public List<string> GetSectionsList()
        {

            //Get The Sections List Method

            List<string> arrSec = new List<string>();
            byte[] buff = new byte[1024];
            GetSectionNamesListA(buff, buff.Length, theFile);
            String s = Encoding.Default.GetString(buff);
            String[] names = s.Split('\0');
            foreach (String name in names)
            {
                if (name != String.Empty)
                {
                    arrSec.Add(name);
                }
            }
            return arrSec;
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        public void SetValue(string section, string key, string value)
        {
            WritePrivateProfileString(section ?? EXE, key, value, theFile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        /// <param name="file">The initialization file path.</param>
        /// <param name="commentDelimiter">The comment delimiter string (default value is ";").
        /// </param>
        public IniFile(string file, string commentDelimiter = ";")
        {
            CommentDelimiter = commentDelimiter;
            TheFile = file;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        public IniFile()
        {
            CommentDelimiter = ";";
        }

        /// <summary>
        /// The comment delimiter string (default value is ";").
        /// </summary>
        public string CommentDelimiter { get; set; }

        private string theFile = null;

        /// <summary>
        /// The initialization file path.
        /// </summary>
        public string TheFile
        {
            get
            {
                return theFile;
            }
            set
            {
                theFile = null;
                dictionary.Clear();
                if (File.Exists(value))
                {
                    theFile = value;
                    using (StreamReader sr = new StreamReader(theFile))
                    {
                        string line, section = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.Length == 0) continue;  // empty line
                            if (!String.IsNullOrEmpty(CommentDelimiter) && line.StartsWith(CommentDelimiter))
                                continue;  // comment

                            if (line.StartsWith("[") && line.Contains("]"))  // [section]
                            {
                                int index = line.IndexOf(']');
                                section = line.Substring(1, index - 1).Trim();
                                continue;
                            }

                            if (line.Contains("="))  // key=value
                            {
                                int index = line.IndexOf('=');
                                string key = line.Substring(0, index).Trim();
                                string val = line.Substring(index + 1).Trim();
                                string key2 = String.Format("[{0}]{1}", section, key).ToLower();

                                if (val.StartsWith("\"") && val.EndsWith("\""))  // strip quotes
                                    val = val.Substring(1, val.Length - 2);

                                if (dictionary.ContainsKey(key2))  // multiple values can share the same key
                                {
                                    index = 1;
                                    string key3;
                                    while (true)
                                    {
                                        key3 = String.Format("{0}~{1}", key2, ++index);
                                        if (!dictionary.ContainsKey(key3))
                                        {
                                            dictionary.Add(key3, val);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dictionary.Add(key2, val);
                                }
                            }
                        }
                    }
                }
            }
        }

        // "[section]key"   -> "value1"
        // "[section]key~2" -> "value2"
        // "[section]key~3" -> "value3"
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        private bool TryGetValue(string section, string key, out string value)
        {
            TheFile = theFile;
            string key2;
            if (section.StartsWith("["))
                key2 = String.Format("{0}{1}", section, key);
            else
                key2 = String.Format("[{0}]{1}", section, key);

            return dictionary.TryGetValue(key2.ToLower(), out value);
        }

        /// <summary>
        /// Gets a string value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        /// <seealso cref="GetAllValues"/>
        public string GetValue(string section, string key, string defaultValue = "")
        {
            string value;
            if (!TryGetValue(section, key, out value))
                return defaultValue;

            return value;
        }

        public string[] GetValueArray(string section, string key, char separator = ',')
        {
            string value;
            string[] values;
            if (!TryGetValue(section, key, out value))
                return null;
            values = value.Split(separator);
            return values;
        }

        public string[] GetValueArray(string section, int key, char separator = ',')
        {
            string value;
            string[] values;
            if (!TryGetValue(section, key.ToString(), out value))
                return null;
            values = value.Split(separator);
            return values;
        }

        public UInt16[] GetUInt16Array(string section, string key, char separator = ',')
        {
            string value;
            string[] stringValues;

            if (!TryGetValue(section, key, out value))
                return null;
            stringValues = value.Split(separator);
            UInt16[] values = new UInt16[stringValues.Length];
            for (int i = 0; i < stringValues.Length; i++)
            {
                values[i] = Convert.ToUInt16(stringValues[i]);
            }
            return values;
        }

        public int[] GetIntArray(string section, string key, char separator = ',')
        {
            string value;
            string[] stringValues;

            if (!TryGetValue(section, key, out value))
                return null;
            stringValues = value.Split(separator);
            int[] values = new int[stringValues.Length];
            for (int i = 0; i < stringValues.Length; i++)
            {
                values[i] = Convert.ToInt16(stringValues[i]);
            }
            return values;
        }

        /// <summary>
        /// Gets a string value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <seealso cref="GetValue"/>
        public string this[string section, string key]
        {
            get
            {
                return GetValue(section, key);
            }
        }

        /// <summary>
        /// Gets an integer value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="minValue">Optional minimum value to be enforced.</param>
        /// <param name="maxValue">Optional maximum value to be enforced.</param>
        /// <returns>The value.</returns>
        public int GetInteger(string section, string key, int defaultValue = 0,
            int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            int value;
            if (!int.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (int)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public UInt16 GetUInt16(string section, string key, UInt16 defaultValue = 0,
            UInt16 minValue = UInt16.MinValue, UInt16 maxValue = UInt16.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            UInt16 value;
            if (!UInt16.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (UInt16)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public short GetShort(string section, string key, short defaultValue = 0,
            short minValue = short.MinValue, short maxValue = short.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            short value;
            if (!short.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (short)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public ushort GetUshort(string section, string key, ushort defaultValue = 0,
            ushort minValue = ushort.MinValue, ushort maxValue = ushort.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            ushort value;
            if (!ushort.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (ushort)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public float GetFloat(string section, string key, float defaultValue = 0,
            float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;
            stringValue = stringValue.Replace('.', ',');
            float value;
            if (!float.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (byte)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public byte GetByte(string section, string key, byte defaultValue = 0,
            byte minValue = byte.MinValue, byte maxValue = byte.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            byte value;
            if (!byte.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (byte)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        /// <summary>
        /// Gets a double floating-point value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="minValue">Optional minimum value to be enforced.</param>
        /// <param name="maxValue">Optional maximum value to be enforced.</param>
        /// <returns>The value.</returns>
        public double GetDouble(string section, string key, double defaultValue = 0,
            double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;
            stringValue = stringValue.Replace('.', ',');
            double value;
            if (!double.TryParse(stringValue, out value))
                return defaultValue;

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        /// <summary>
        /// Gets a boolean value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        public bool GetBoolean(string section, string key, bool defaultValue = false)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            return (stringValue != "0" && !stringValue.StartsWith("f", true, null));
        }

        /// <summary>
        /// Gets an array of string values by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The array of values, or null if none found.</returns>
        /// <seealso cref="GetValue"/>
        public string[] GetAllValues(string section, string key)
        {
            TheFile = theFile;
            string key2, key3, value;
            if (section.StartsWith("["))
                key2 = String.Format("{0}{1}", section, key).ToLower();
            else
                key2 = String.Format("[{0}]{1}", section, key).ToLower();

            if (!dictionary.TryGetValue(key2, out value))
                return null;

            List<string> values = new List<string>();
            values.Add(value);
            int index = 1;
            while (true)
            {
                key3 = String.Format("{0}~{1}", key2, ++index);
                if (!dictionary.TryGetValue(key3, out value))
                    break;
                values.Add(value);
            }

            return values.ToArray();
        }
        public void DeleteKey(string Key, string Section = null)
        {
            SetValue(Section ?? EXE, Key, null);
        }

        public void DeleteSection(string Section = null)
        {
            SetValue(Section ?? EXE, null, null);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return GetValue(Key, Section).Length > 0;
        }
    }

    #region Eccezioni 
    public class IniFileNotFoundException : Exception
    {
        public IniFileNotFoundException() { }
        public IniFileNotFoundException(string message) : base(message) { }
    }

    public class IniParameterValueInvalid : Exception
    {
        public IniParameterValueInvalid() { }
        public IniParameterValueInvalid(string message) : base(message) { }
    }
    #endregion

}
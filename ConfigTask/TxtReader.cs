using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTask
{
    public class TxtReader
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public TxtReader Parent { get; set; }

        public static List<TxtReader> AllItems;

        public List<TxtReader> Items
        {
            get
            {
                var result = this.Parent == null ? AllItems.Where(x => x.Parent == this || x.Parent == null).ToList() : AllItems.Where(x => x.Parent == this).ToList();
                return result;
            }
        }

        static TxtReader()
        {
            AllItems = new List<TxtReader>();
        }

        public TxtReader(string path)
        {
            Path = path;
            Name = String.Empty;
            Value = String.Empty;

            ParseFile();
        }

        public TxtReader(string name, string value, TxtReader parent)
        {
            Name = name;
            Value = value;
            Parent = parent;
        }

        public TxtReader this[string index]
        {
            get
            {
                foreach (var item in Items)
                {
                    if (item.Name == index)
                        return item;
                }

                return null;
            }
        }

        public void ParseFile()
        {
            try
            {
                var lines = File.ReadAllLines(Path);

                foreach (var line in lines)
                {
                    TxtReader parent = null;
                    var splittedLines = line.Split('=');

                    var leftLines = splittedLines[0].Split('.');

                    for (int i = 0; i < leftLines.Length; i++)
                    {
                        parent = Add(leftLines[i], splittedLines[1], parent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TxtReader Add(string name, string value, TxtReader parent)
        {
            var existingElement = AllItems.Where(x => x.Name == name && x.Parent == parent).SingleOrDefault();

            if (existingElement != null)
            {
                return existingElement;
            }
            else
            {
                TxtReader newItem = new TxtReader(name, value, parent);

                Items.Add(newItem);

                AllItems.Add(newItem);

                return newItem;
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}

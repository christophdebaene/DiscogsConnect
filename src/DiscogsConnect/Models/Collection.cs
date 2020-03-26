using System;
using System.Collections.Generic;

namespace DiscogsConnect
{
    public class Folder : Resource
    {
        public string Count { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Count})";
        }
    }

    public class FolderResponse
    {
        public List<Folder> Folders { get; set; }
    }

    public class AddToCollectionResponse
    {
        public int InstanceId { get; set; }
        public string ResourceUrl { get; set; }
    }

    public class FieldsResponse
    {
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
        public bool Public { get; set; }
    }

    public class CollectionValue
    {
        public string Maximum { get; set; }
        public string Median { get; set; }
        public string Minimum { get; set; }
    }

    public class CollectionItem
    {
        public int Id { get; set; }
        public int InstanceId { get; set; }
        public DateTime DateAdded { get; set; }
        public int FolderId { get; set; }

        public Information BasicInformation { get; set; }

        public class Information : Resource
        {
            public string Thumb { get; set; }
            public string Title { get; set; }
            public string CoverImage { get; set; }
            public int Year { get; set; }

            public List<Release.Label> Labels { get; set; }
            public List<Release.Format> Formats { get; set; }
            public List<Release.Artist> Artists { get; set; }
        }
    }
}
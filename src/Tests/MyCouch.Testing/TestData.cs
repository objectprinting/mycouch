﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MyCouch.Testing.Model;

namespace MyCouch.Testing
{
    public static class TestData
    {
        public static class Json
        {
            public const string Artist1Id = "sample:1";
            public const string Artist2Id = "sample:2";

            public const string Artist1 = "{\"_id\": \"sample:1\", \"$doctype\": \"artist\", \"name\": \"Fake artist 1\", \"albums\":[{\"name\": \"Greatest fakes #1\"}]}";
            public const string Artist2 = "{\"_id\": \"sample:2\", \"$doctype\": \"artist\", \"name\": \"Fake artist 2\", \"albums\":[{\"name\": \"Greatest fakes #2\"},{\"name\": \"Greatest fakes #3\"}]}";
        }

        public static class Views
        {
            public const string ArtistsAlbums =
                "{" +
                    "\"_id\": \"_design/artists\"," +
                    "\"language\": \"javascript\"," +
                    "\"views\": {" +
                        "\"albums\": {" +
                            "\"map\": \"function(doc) {  if(!doc.$doctype === 'artist') return;  emit(doc.name, doc.albums);}\"" +
                        "}" +
                    "}" +
                "}";
        }

        public static Artist CreateArtist()
        {
            return CreateArtists(1).Single();
        }

        public static Artist[] CreateArtists(int numOf)
        {
            var artists = new List<Artist>();
            var numOfAlbums = new[] { 1, 2, 3 };

            for (var c = 0; c < numOf; c++)
            {
                var artist = new Artist
                {
                    ArtistId = (c + 1).ToString(),
                    Name = "Artist #" + (c + 1)
                };

                artist.Albums = CreateAlbums(numOfAlbums[c % numOfAlbums.Length], artist.Name);

                artists.Add(artist);
            }

            return artists.ToArray();
        }

        private static Album[] CreateAlbums(int numOf, string artistName)
        {
            var albums = new List<Album>();

            for (var c = 0; c < numOf; c++)
            {
                albums.Add(new Album
                {
                    Name = string.Format("{0} - Greatest fakes #{1}", artistName, c + 1)
                });
            }

            return albums.ToArray();
        }

        public static Stream AsStream(this string json)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
    }
}
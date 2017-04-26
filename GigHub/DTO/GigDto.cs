using System;

namespace GigHub.DTO
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool isCanceled { get; set; }

        public ApplicationUserDto Artist { get; set; }


        public DateTime DateTime { get; set; }


        public string Venue { get; set; }


        public GenreDto Genre { get; set; }


    }
}
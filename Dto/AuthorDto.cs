using System.Collections.Generic;

namespace EFCore3.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
		public string AuthorName { get; set; }

		public virtual List<BookDto> Books { get; set; }
    }
}
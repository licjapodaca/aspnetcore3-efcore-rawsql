using System.Collections.Generic;

namespace EFCore3.Entities
{
    public class Author
    {
        public int Id { get; set; }
		public string AuthorName { get; set; }

		public virtual List<Book> Books { get; set; }
    }
}
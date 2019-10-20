namespace EFCore3.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
		public string ISBN { get; set; }
		public string BookName { get; set; }

		public virtual AuthorDto Author { get; set; }
    }
}
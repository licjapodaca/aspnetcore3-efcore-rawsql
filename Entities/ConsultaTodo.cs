namespace EFCore3.Entities
{
    public class ConsultaTodo
    {
        public int AuthorId { get; set; }
		public string AuthorName { get; set; }
		public int BookId { get; set; }
		public string ISBN { get; set; }
		public string BookName { get; set; }
    }
}
namespace EFCore3.Entities
{
    public class Book
    {
        public int Id { get; set; }
		public string ISBN { get; set; }
		public string BookName { get; set; }

		public virtual Author Author { get; set; }
    }
}
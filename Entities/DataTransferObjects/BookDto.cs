namespace Entities.DataTransferObjects
{

    [Serializable]
    public record BookDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public int Price { get; set; }
    }

}

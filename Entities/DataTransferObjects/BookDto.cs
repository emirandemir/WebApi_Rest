namespace Entities.DataTransferObjects
{

    [Serializable]
    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; init; }
    }

}

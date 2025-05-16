namespace Policies.Core.Dtos
{
    public class BadRequestDto
    {
        public string Title { get; set; }
        public string TraceId { get; set; }
        public Object Errors { get; set; }
    }
}

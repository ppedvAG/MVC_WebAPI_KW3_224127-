using ProtoBuf;

namespace WebAPI_ControllerSamples.Models
{
    [ProtoContract]
    public class Movie
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Title { get; set; }

        [ProtoMember(3)]
        public string Description { get; set; }

        [ProtoMember(4)]
        public decimal Price { get; set; }
    }
}

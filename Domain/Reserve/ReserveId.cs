namespace modeling_mtg_room.Domain.Reserve
{
    public class ReserveId
    {
        public string Value { get; }

        public ReserveId(string value)
        {
            // todo: ルールいまのところ無し
            this.Value = value;
        }
    }
}
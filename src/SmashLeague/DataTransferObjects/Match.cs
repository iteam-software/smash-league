
namespace SmashLeague.DataTransferObjects
{
    public class MatchDto
    {
        public int MatchId { get; set; }
        public TeamLazyDto[] Teams { get; set; }
        public int WinnerId { get; set; }
        public int SeriesId { get; set; }
    }
}

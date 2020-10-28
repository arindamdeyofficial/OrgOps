namespace SGRE.SiteEnrichment.RepositoryLayer.Geography.DbModels
{
    public partial class CourseDbEntity
    {
        public CourseDbEntity()
        {
            
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDesc { get; set; }
    }
}

namespace TeacherAndClassRoomWebApi.ApiModel
{
    public class TeacherAndClassroomApiModel
    {
        public TeacherApiModel Teacher { get; set; }
        public List<ClassroomApiModel> Classrooms { get; set; }
    }
}

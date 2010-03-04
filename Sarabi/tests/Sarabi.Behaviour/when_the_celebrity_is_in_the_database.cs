namespace Sarabi.Behaviour
{
    //public class when_the_celebrity_is_in_the_database 
    //{
  
    //    private static Celebrity celebrity;
    //    protected static GetCelebrityInfoCommand get_celebrity_info;
    //    private static Mock<ICelebrityRepository> mockCelebrityRepository;
    //    private const string _celebrityName = "Britney Spears";

    //    Because of = () =>
    //                     {
    //                         mockCelebrityRepository = new Mock<ICelebrityRepository>();
    //                         get_celebrity_info = new GetCelebrityInfoCommand(mockCelebrityRepository.Object);
    //                         get_celebrity_info.CelebrityName = _celebrityName;


    //                         mockCelebrityRepository.Setup(m => m.GetCelebrityByName(_celebrityName)).Returns(
    //                             new Celebrity(){Name = _celebrityName});

    //                     };


    //    It should_return_the_celebrity_from_the_database = () => get_celebrity_info.Execute().ShouldNotBeNull();

    //    It should_return_the_celebrity_from_the_database_with_the_correct_name = () => get_celebrity_info.Execute().Name.ShouldEqual(_celebrityName);
    //}
    
}
using System;
using System.IO;
using System.Text;
using Machine.Specifications;
using Sarabi.ApplicationServices;
using Sarabi.ApplicationServices.Contributors;
using Sarabi.Core;
using Sarabi.Data;

namespace Sarabi.Behaviour.ApplicationServices.CelebFinder.Contributors
{
    public class ResourceValidatorTests
    {
        public class ResourceValidatorContext
        {
            private readonly static Stream _typesCsv = new MemoryStream(
                Encoding.ASCII.GetBytes(
                    "The_Beatles\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Band\tr\n" +
                    "The_Libertines\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Band\tr\n" +
                    "Britney_Spears\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Person\tr\n" +
                    "John_Prescott\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Person\tr\n"));

            private static readonly CelebFinderContext _context = new CelebFinderContext();

            
            public static void Execute()
            {
                new ResourceTypeValidator(new DbpediaTypesRepository(_typesCsv)).Execute(_context);
            }

            protected static void wikipedia_url_is(string wikipediaUrl)
            {
                _context.WikipediaUrl = wikipediaUrl;
                Execute();
            }

            protected static void the_type_should_be(string type)
            {
                _context.Celebrity.Type.ShouldEqual(type.ToLower());
            }
        }

        public class When_given_a_persons_wikipedia_url : ResourceValidatorContext
        {
            Because the = () => wikipedia_url_is("http://en.wikipedia.org/wiki/Britney_Spears");

            It should_get_the_celeb_information = () => the_type_should_be("Person");
        }

        public class When_given_a_bands_wikipedia_url : ResourceValidatorContext
        {
            Because the = () => wikipedia_url_is("http://en.wikipedia.org/wiki/The_Beatles");

            It should_get_the_band_information = () => the_type_should_be("Band");
        }   
    }
}
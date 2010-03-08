using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Machine.Specifications;
using Sarabi.Data;

namespace Sarabi.Behaviour.Data
{
    public class DbpediaTypesRepositoryTests
    {
        public class DbpediaTypesRepositoryContext
        {
            private readonly static Stream _typesCsv = new MemoryStream(
                Encoding.ASCII.GetBytes(
                    "The_Beatles\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Band\tr\n" +
                    "The_Libertines\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Band\tr\n" +
                    "Britney_Spears\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Person\tr\n" +
                    "John_Prescott\thttp://www.w3.org/1999/02/22-rdf-syntax-ns#type\thttp://dbpedia.org/ontology/Person\tr\n"));


            private static string _type;

            protected static void tried_to_get_the_type_for(string resource)
            {
                _type = new DbpediaTypesRepository(_typesCsv).GetResourceType(resource);
            }

            protected static void the_type(string type)
            {
                if (false == string.IsNullOrEmpty(type))
                    type = type.ToLower();
                
                type.ShouldEqual(_type);
            }
        }

        public class When_getting_the_type_for_britney_spears : DbpediaTypesRepositoryContext
        {
            Because we = () => tried_to_get_the_type_for("Britney_Spears");
            It should_return = () => the_type("Person");
        }

        public class When_getting_the_type_for_the_beatles : DbpediaTypesRepositoryContext
        {
            Because we = () => tried_to_get_the_type_for("The_Beatles");
            It should_return = () => the_type("Band");
        }

        public class When_the_resource_type_is_not_known : DbpediaTypesRepositoryContext
        {
            Because we = () => tried_to_get_the_type_for("F1");
            It should_return = () => the_type(null);
        }

        public class When_getting_the_type_for_a_resource_with_incorrect_type : DbpediaTypesRepositoryContext
        {
            Because we = () => tried_to_get_the_type_for("john_pResCott");
            It should_return = () => the_type("Person");
        }

        public class When_loading_actual_data_it_works_of
        {
            Because we = () =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                _repository = new DbpediaTypesRepository();
                stopwatch.Stop();
                _elapsedTime = stopwatch.ElapsedMilliseconds;
            };

            It took_a_resonable_amount_of_time = () => _elapsedTime.ShouldBeLessThan(3000);
            It can_load_a_resource = () => _repository.GetResourceType("Bill_Gates").ShouldNotBeNull();

            private static DbpediaTypesRepository _repository;
            private static long _elapsedTime;
        }
    }
}
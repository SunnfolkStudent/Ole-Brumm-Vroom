using UnityEngine;
using System.ComponentModel;

// We have to insert the following below if you want to use records
// TODO: Use the Init with record, I think?

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit {}
}

namespace Testing_Scripts
{
    public class TestingRecords : MonoBehaviour
    {
        public record Person (string firstName, int index)
        {
            private readonly Person _person = new("Peter", 1);
            private readonly Person _person1 = new("Peter", 1);
            private readonly Person _person2 = new("John", 3);

            public Person(Person person)
            {
                if (_person == _person1)
                {
                    Debug.Log("Matching name and index!");
                }
                // Matching name and index!
                                
                var (firstName, index) = _person2;
                Debug.Log($"name: {firstName}, index: {index}");
                // name: John, index: 3
            }
        }
    }
}

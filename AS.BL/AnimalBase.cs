using System;


namespace AS.BL
{
    // Accepted animal type
    public enum AcceptedAnimalOption
    {
        cat,
        dog,
        bird,
        snake
    }

    // Animal's State in the Shelter
    public enum AnimalStateOption
    {
        CheckedIn,
        Returned
    }

    public abstract class AnimalBase
    {
        private AcceptedAnimalOption AcceptedAnimal { get; set; }
        public  AnimalStateOption InShelterState { get; set; }
        public bool IsValid => Validate();

        public abstract bool Validate();
    }
}

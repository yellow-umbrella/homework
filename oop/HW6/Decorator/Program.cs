using System;
namespace Decorator.Examples
{
    class MainApp
    {
        static void Main()
        {
            ChristmasTree tree = new ChristmasTree();
            Lights decorWithLights = new Lights("yellow");
            Toy decorWithBall = new Toy("ball");
            Toy decorWithCandy = new Toy("candy");

            decorWithBall.SetComponent(tree);
            decorWithLights.SetComponent(decorWithBall);
            decorWithCandy.SetComponent(decorWithLights);

            decorWithCandy.Operation();

            // Wait for user
            Console.Read();
        }
    }

    class ChristmasTree : Component
    {
        public override void Operation()
        {
            Console.WriteLine("Christmas tree: ");
        }
    }

    class Lights : Decorator
    {
        private bool isLightsOn = true;
        private string color;

        public Lights(string color)
        {
            this.color = color;
        }

        public override void Operation()
        {
            base.Operation();
            AddedBehavior();   
        }
        void AddedBehavior()
        {
            if (isLightsOn)
            {
                Console.WriteLine(color + " lights on");
                isLightsOn = false;
            }
            else
            {
                Console.WriteLine(color + " lights off");
                isLightsOn = true;
            }
        }
    }

    class Toy : Decorator
    {
        private string addedState;

        public Toy(string type)
        {
            addedState = type;
        }

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine(addedState);
        }
    }

    class DecoratedChristmasTree : Decorator
    {
        public override void Operation()
        {
            base.Operation();
        }
    }

    // "Component"
    abstract class Component
    {
        public abstract void Operation();
    }

    // "Decorator"
    abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

}

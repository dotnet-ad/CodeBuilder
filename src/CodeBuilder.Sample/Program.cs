using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace CodeBuilder.Sample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var iViewModel = new Interface("IViewModel")
                                .WithInterface<INotifyPropertyChanged>()
                                .WithAttribute(new Type(new Module("Test"), "Example"))
                                .WithAttribute(new Type(new Module("Other"), "Sample"), "arg1", "arg2")
                                .WithProperty<string>("Test", x => x.WithDocumentation("A test property."))
                                .WithProperty<string>("Test2", x => x.WithDocumentation("A test property."))
                                .WithEvent("Updated", initializer: x => x.WithDocumentation("When updated"))
                                .WithEvent("Updated2")
                                .WithMethod<Task>("UpdateAsync", x =>
                                {
                                    return x.WithParameter<DateTime>("date", p => p.WithDocumentation("The last updated date")
                                .WithAttribute(new Type(new Module("Serializers"), "Body")))
                                            .WithParameter<CancellationToken>("token")
                                            .WithAttribute(new Type(new Module("Test"), "Example"))
                                            .WithDocumentation("Updates the current state of the view model.");
                                });

            var sampleViewModel = new Class("SampleViewModel")
                                .WithConstructor(x => x.WithParameter<string>("test").WithBody(new Statement("this.test = test;")))
                                .WithInterface(iViewModel)
                                .WithField<bool>("test2")
                                .WithField<string>("test")
                                .WithEvent<PropertyChangedEventHandler>("PropertyChanged")
                                .WithEvent("Updated", initializer: x => x.WithDocumentation("When updated"))
                                .WithAutoProperty<int>("AutoProperty")
                                .WithProperty<string>("Test", x =>
                                {
                                    return x.WithFieldGetter("test")
                                            .WithSetter(new Block(new Statement("this.test = value;")))
                                            .WithDocumentation("A test property.");
                                })
                                .WithAsyncMethod<Task>("UpdateAsync", x =>
                                {
                                    return x.WithParameter<DateTime>("date")
                                            .WithParameter<CancellationToken>("token")
                                            .WithDocumentation("Updates the current state of the view model.")
                                            .WithBody(new Block(new Statement("await Task.Delay(3);"), new Statement("this.Updated?.Invoke(this, System.EventArgs.Empty);")));
                                });

            var module = new Module("CodeBuilder.Sample").WithTypes(iViewModel, sampleViewModel)
                                                         .WithImport(Modules.ThreadingTasks)
                                                         .WithImport("System.ComponentModel");

            var generator = new CsharpGenerator();

            Console.WriteLine(generator.Generate(module));
        }
    }
}

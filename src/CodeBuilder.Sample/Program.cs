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
                                .WithProperty<string>("Test", x => x.WithDocumentation("A test property."))
                                .WithEvent("Updated")
                                .WithMethod<Task>("UpdateAsync", x =>
                                {
                                    return x.WithParameter<DateTime>("date", p => p.WithDocumentation("The last updated date"))
                                            .WithParameter<CancellationToken>("token")
                                            .WithDocumentation("Updates the current state of the view model.");
                                });

            var sampleViewModel = new Class("SampleViewModel")
                                .WithInterface(iViewModel)
                                .WithField<bool>("test2")
                                .WithField<string>("test")
                                .WithEvent<PropertyChangedEventHandler>("PropertyChanged")
                                .WithEvent("Updated")
                                .WithProperty<int>("AutoProperty", x => x.WithAutoGetter().WithAutoSetter())
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
                                })
                                .WithMethod<string>("Test", x => x.WithImplementation(ImplementationModifier.MultiFiles));

            var module = new Module("CodeBuilder.Sample").WithTypes(iViewModel,sampleViewModel)
                                                         .WithImport(Modules.ThreadingTasks)
                                                         .WithImport("System.ComponentModel");

            var generator = new CsharpGenerator();

            Console.WriteLine(generator.Generate(module));
        }
    }
}

# Code.Builder

A set of helper classes for generating code.

## Install

Available on NuGet

[![NuGet](https://img.shields.io/nuget/v/Code.Builder?label=NuGet)](https://www.nuget.org/packages/Code.Builder/)

## Quickstart

```csharp
var iViewModel = new Interface("IViewModel")
                                .WithInterface<INotifyPropertyChanged>()
                                .WithProperty<string>("Test", x => x.WithDocumentation("A test property."))
                                .WithProperty<string>("Test2", x => x.WithDocumentation("A test property."))
                                .WithEvent("Updated", initializer: x => x.WithDocumentation("When updated"))
                                .WithEvent("Updated2")
                                .WithMethod<Task>("UpdateAsync", x =>
                                {
                                    return x.WithParameter<DateTime>("date", p => p.WithDocumentation("The last updated date"))
                                            .WithParameter<CancellationToken>("token")
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

            var module = new Module("CodeBuilder.Sample").WithTypes(iViewModel,sampleViewModel)
                                                         .WithImport(Modules.ThreadingTasks)
                                                         .WithImport("System.ComponentModel");

            var generator = new CsharpGenerator();

            Console.WriteLine(generator.Generate(module));
```

**Result**

```csharp
using System.Threading.Tasks;
using System.ComponentModel;

namespace CodeBuilder.Sample
{
    public interface IViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties

        ///<sumary>
        ///A test property.
        ///</sumary>
        System.String Test { get; set; }

        ///<sumary>
        ///A test property.
        ///</sumary>
        System.String Test2 { get; set; }

        #endregion

        #region Events

        ///<sumary>
        ///When updated
        ///</sumary>
        event System.EventHandler Updated;

        event System.EventHandler Updated2;

        #endregion

        #region Methods

        ///<sumary>
        ///Updates the current state of the view model.
        ///</sumary>
        ///<returns>The Task result.</returns>
        ///<param name="date">The last updated date</param>
        ///<param name="token"></param>
        System.Threading.Tasks.Task UpdateAsync(System.DateTime date, System.Threading.CancellationToken token);

        #endregion
    }

    public class SampleViewModel : IViewModel
    {
        #region Fields

        private System.Boolean test2;

        private System.String test;

        #endregion

        #region Constructors

        public SampleViewModel(System.String test) => this.test = test;

        #endregion

        #region Events

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        ///<sumary>
        ///When updated
        ///</sumary>
        public event System.EventHandler Updated;

        #endregion

        #region Properties

        public System.Int32 AutoProperty { get; set; }

        ///<sumary>
        ///A test property.
        ///</sumary>
        public System.String Test
        {
            get => this.test;

            set
            {
                this.test = value;
            }
        }

        #endregion

        #region Methods

        ///<sumary>
        ///Updates the current state of the view model.
        ///</sumary>
        ///<returns>The Task result.</returns>
        ///<param name="date"></param>
        ///<param name="token"></param>
        public async System.Threading.Tasks.Task UpdateAsync(System.DateTime date, System.Threading.CancellationToken token)
        {
            await Task.Delay(3);
            this.Updated?.Invoke(this, System.EventArgs.Empty);
        }

        #endregion
    }
}
```

## Q&A

#### Why not using Roslyn?

> Roselyn is far too complex and heavy for several scenarios where you just want to have basic code generation. This should also allows us to have more target languages easily.


## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)

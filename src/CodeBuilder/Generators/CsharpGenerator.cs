using System;
using System.Text;
using System.Linq;
namespace CodeBuilder
{
    public class CsharpGenerator : ICodeGenerator
    {
        public CsharpGenerator()
        {
        }

        public int Indentation { get; set; } = 4;

        private int scope;

        private StringBuilder builder;

        public string Generate(Module module)
        {
            scope = 0;
            builder = new StringBuilder();

            if(module.Imports.Any())
            {
                foreach (var import in module.Imports)
                {
                    this.AppendLine("using " + import.Name + ";");
                }
                this.NewLine();
            }

            this.AppendLine($"namespace {module.Name}");
            this.AppendBlock(() =>
            {
                for (int i = 0; i < module.Types.Length; i++)
                {
                    if (i > 0) this.NewLine();
                    var type = module.Types[i];
                    switch (type)
                    {
                        case Class c:
                            this.Append(c);
                            break;
                        case Interface interf:
                            this.Append(interf);
                            break;
                    }
                }
            });

            return builder.ToString();

        }

        private void Append(Interface interf)
        {
            this.AppendDocumentationSummary(interf.Documentation);

            this.Scope().Append($"public interface {interf.Name}");

            if (interf.Interfaces.Any())
            {
                this.Append(" : ");
                for (int i = 0; i < interf.Interfaces.Length; i++)
                {
                    if (i > 0) this.Append(", ");
                    var parent = this.GetFullname(interf.Interfaces[i]);
                    this.Append(parent);
                }
            }

            this.NewLine();

            this.AppendBlock(() =>
            {
                if (interf.Properties.Any())
                {
                    this.AppendLine("#region Properties");

                    for (int i = 0; i < interf.Properties.Length; i++)
                    {
                        var property = interf.Properties[i];
                        var type = this.GetFullname(property.Type);

                        this.NewLine();
                        this.AppendDocumentationSummary(property.Documentation);
                        this.Scope().Append(type).Append(" ").Append(property.Name).Append(" {");

                        if (property.Getter != null)
                        {
                            this.Append(" get;");
                        }

                        if (property.Setter != null)
                        {
                            this.Append(" set;");
                        }

                        this.Append(" }");
                    }

                    this.NewLine();

                    this.AppendLine("#endregion");
                }

                if (interf.Events.Any())
                {
                    this.AppendLine("#region Events");

                    for (int i = 0; i < interf.Events.Length; i++)
                    {
                        var ev = interf.Events[i];
                        var handlerType = this.GetFullname(ev.HandlerType);

                        this.NewLine();
                        this.AppendDocumentationSummary(ev.Documentation);
                        this.Scope().Append("event ").Append(handlerType).Append(" ").Append(ev.Name).Append(";");
                    }

                    this.NewLine();

                    this.AppendLine("#endregion");
                }

                var publicInstanceMethods = interf.Methods.Where(x => x.Scope == ScopeModifier.Instance && x.Access == AccessModifier.Public).ToArray();
                if (publicInstanceMethods.Any())
                {
                    this.AppendLine("#region Methods");

                    for (int i = 0; i < publicInstanceMethods.Length; i++)
                    {
                        var method = publicInstanceMethods[i];
                        var returnType = this.GetFullname(method.ReturnType);

                        this.NewLine();

                        if(!string.IsNullOrEmpty(method.Documentation))
                        {
                            this.AppendDocumentationSummary(method.Documentation);

                            if (method.ReturnType != null)
                            {
                                this.AppendCommentLine($"<returns>The {method.ReturnType.Name} result.</returns>");
                            }

                            foreach (var parameter in method.Parameters)
                            {
                                this.AppendCommentLine($"<param name=\"{parameter.Name}\">{parameter.Documentation}</param>");
                            }
                        }

                        this.Scope().Append(returnType).Append(" ").Append(method.Name).Append("(");

                        for (int pi = 0; pi < method.Parameters.Length; pi++)
                        {
                            var parameter = method.Parameters[pi];

                            if (pi > 0) 
                            {
                                this.Append(", ");
                            }
                            else if(method.Scope == ScopeModifier.Extension)
                            {
                                this.Append("this ");
                            }

                            var paramType = this.GetFullname(parameter.Type);
                            this.Append(paramType).Append(" ").Append(parameter.Name);
                        }

                        this.Append(");");
                    }

                    this.NewLine();

                    this.AppendLine("#endregion");
                }
            });
        }

        private void Append(Class @class)
        {
            this.AppendDocumentationSummary(@class.Documentation);

            this.Scope().Append(this.Get(@class.Access)).Append(" ").Append(this.Get(@class.Implementation)).Append(this.Get(@class.Scope));
            this.Append($"class {@class.Name}");

            if (@class.Parent != null)
            {
                this.Append(" : ");
                var parent = this.GetFullname(@class.Parent);
                this.Append(parent);
            }

            if (@class.Interfaces.Any())
            {
                if (@class.Parent == null) this.Append(" : ");
                for (int i = 0; i < @class.Interfaces.Length; i++)
                {
                    if (i > 0 || @class.Parent != null) this.Append(", ");
                    var parent = this.GetFullname(@class.Interfaces[i]);
                    this.Append(parent);
                }
            }

            this.NewLine();

            this.AppendBlock(() =>
            {
                var hasFields = @class.Fields.Any();
                var hasEvents = @class.Events.Any();
                var hasProperties = @class.Properties.Any();
                var hasMethods = @class.Methods.Any();

                if (hasFields)
                {
                    this.AppendLine("#region Fields");

                    for (int i = 0; i < @class.Fields.Length; i++)
                    {
                        var field = @class.Fields[i];
                        var type = this.GetFullname(field.Type);

                        this.NewLine().Scope().Append(this.Get(field.Access)).Append(" ").Append(this.Get(field.Scope));
                        this.Append(type).Append(" ").Append(field.Name).Append(";").NewLine();
                    }

                    this.NewLine();

                    this.AppendLine("#endregion");
                }

                if (hasEvents)
                {
                    if (hasFields) this.NewLine();

                    this.AppendLine("#region Events");

                    for (int i = 0; i < @class.Events.Length; i++)
                    {
                        var ev = @class.Events[i];
                        var handlerType = this.GetFullname(ev.HandlerType);

                        this.NewLine();
                        this.AppendDocumentationSummary(ev.Documentation);
                        this.NewLine().Scope().Append(this.Get(ev.Access)).Append(" event ").Append(this.Get(ev.Scope));
                        this.Append(handlerType).Append(" ").Append(ev.Name).Append(";").NewLine();
                    }

                    this.NewLine();

                    this.AppendLine("#endregion");
                }

                if (hasProperties)
                {
                    if (hasEvents || hasFields) this.NewLine();

                    this.AppendLine("#region Properties");

                    for (int i = 0; i < @class.Properties.Length; i++)
                    {
                        var property = @class.Properties[i];
                        var type = this.GetFullname(property.Type);

                        this.NewLine();
                        this.AppendDocumentationSummary(property.Documentation);
                        this.Scope().Append(this.Get(property.Access)).Append(" ").Append(this.Get(property.Scope));
                        this.Append(type).Append(" ").Append(property.Name);


                        var hasGetter = property.Getter != null && property.Getter != Body.None;
                        var hasSetter = property.Setter != null && property.Setter != Body.None;

                        if (property.Getter == Body.Auto || property.Setter == Body.Auto)
                        {
                            this.Append(" {");
                            if (hasGetter) this.Append(" get;");
                            if (hasSetter) this.Append(" set;");
                            this.Append(" }").NewLine();
                        }
                        else
                        {
                            this.NewLine().AppendBlock(() =>
                            {
                                if (hasGetter)
                                {
                                    this.Scope().Append("get");
                                    this.Append(property.Getter);
                                }

                                if (hasSetter)
                                {
                                    if (hasGetter)
                                        this.NewLine();
                                    
                                    this.Scope().Append("set");
                                    this.Append(property.Setter);
                                }
                            });
                        }
                    }

                    this.NewLine().AppendLine("#endregion");
                }


                if (hasMethods)
                {
                    if (hasFields || hasProperties) this.NewLine();

                    this.AppendLine("#region Methods");

                    for (int i = 0; i < @class.Methods.Length; i++)
                    {
                        var method = @class.Methods[i];
                        var returnType = this.GetFullname(method.ReturnType);

                        this.NewLine();

                        if (!string.IsNullOrEmpty(method.Documentation))
                        {
                            this.AppendDocumentationSummary(method.Documentation);

                            if (method.ReturnType != null)
                            {
                                this.AppendCommentLine($"<returns>The {method.ReturnType.Name} result.</returns>");
                            }

                            foreach (var parameter in method.Parameters)
                            {
                                this.AppendCommentLine($"<param name=\"{parameter.Name}\">{parameter.Documentation}</param>");
                            }
                        }

                        this.Scope().Append(this.Get(method.Access)).Append(" ").Append(this.Get(method.Scope)).Append(this.Get(method.Sync));
                        this.Append(returnType).Append(" ").Append(method.Name).Append("(");

                        for (int pi = 0; pi < method.Parameters.Length; pi++)
                        {
                            if (pi > 0) this.Append(", ");

                            var parameter = method.Parameters[pi];
                            var paramType = this.GetFullname(parameter.Type);
                            this.Append(paramType).Append(" ").Append(parameter.Name);
                        }

                        this.Append(")");

                        if (method.Body != null && method.Body != Body.None)
                        {
                            this.NewLine().Append(method.Body);
                        }
                        else
                        {
                            this.Append(" {}");
                            this.NewLine();
                        }
                    }

                    this.NewLine().AppendLine("#endregion");

                }
            });
        }

        private void Append(Body body)
        {
            if(body != null && body != Body.None)
            {
                if (body.Root is Statement statement)
                {
                    this.Append(" => ");
                }

                this.Append(body.Root);
            }
            else
            {
                this.Append(" {}");
            }
        }

        private void Append(IInstruction instruction)
        {
            if (instruction is Block block)
            {
                this.NewLine().Append(block);
            }

            else if (instruction is Statement statement)
            {
                this.Append(statement);
            }
        }

        private CsharpGenerator Append(Block block)
        {
            this.AppendBlock(() =>
            {
                foreach (var instruction in block.Instructions)
                {
                    this.Scope().Append(instruction);
                    this.NewLine();
                }
            });
            return this;
        }

        private CsharpGenerator Append(Statement statement)
        {
            return this.Append(statement.Content);
        }

        /// <summary>
        /// Gets the fullname.
        /// </summary>
        /// <returns>The fullname.</returns>
        /// <param name="type">Type.</param>
        private string GetFullname(IType type)
        {
            switch (type)
            {
                case GenericType m:
                    var parameterTypeNames = m.Parameters.Select(p => GetFullname(p));
                    return $"{m.Fullname}<{string.Join(", ", parameterTypeNames)}>";
                case Type m: return m.Fullname;
                case BaseType b: return b.Name;
                default: return type.Name;
            }
        }

        #region Modifiers

        private string Get(AccessModifier access)
        {
            switch (access)
            {
                case AccessModifier.Private:
                    return "private";

                case AccessModifier.Protected:
                    return "protected";
                default:
                    return "public";
            }
        }

        private string Get(ScopeModifier scope)
        {
            switch (scope)
            {
                case ScopeModifier.Extension:
                case ScopeModifier.Static:
                    return "static ";
                default:
                    return "";
            }
        }

        private string Get(SyncModifier async)
        {
            switch (async)
            {
                case SyncModifier.Asynchronous:
                    return "async ";
                default:
                    return "";
            }
        }


        private string Get(ImplementationModifier implementation)
        {
            switch (implementation)
            {
                case ImplementationModifier.MultiFiles:
                    return "partial ";
                default:
                    return "";
            }
        }

        #endregion

        #region Helpers

        private CsharpGenerator Scope()
        {
            builder.Append(new String(' ', scope * this.Indentation));
            return this;
        }

        private CsharpGenerator NewLine()
        {
            builder.AppendLine();
            return this;
        }

        private CsharpGenerator Append(string line)
        {
            builder.Append(line);
            return this;
        }

        private CsharpGenerator AppendLine(string line, bool scoped = true)
        {
            if (scoped) this.Scope();
            return this.Append(line).NewLine();
        }

        private CsharpGenerator AppendCommentLine(string line, bool scoped = true)
        {
            return this.AppendLine("///" + line, scoped);
        }

        private CsharpGenerator AppendDocumentationSummary(string doc)
        {
            if (!string.IsNullOrEmpty(doc))
            {
                this.AppendCommentLine("<sumary>");
                this.AppendCommentLine(doc); // TODO split by columns
                this.AppendCommentLine("</sumary>");
            }

            return this;
        }

        private void AppendBlock(Action action)
        {
            this.AppendLine("{");
            this.scope++;
            action();
            this.scope--;
            this.AppendLine("}");

        }

        #endregion
    }
}

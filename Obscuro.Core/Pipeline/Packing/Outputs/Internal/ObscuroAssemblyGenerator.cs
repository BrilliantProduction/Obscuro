// NOTE: All is broken since .NET Core or .NET 5
// NEED TO RETHINK THIS OVER


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Reflection.Emit;

//using Obscuro.Abstract;

//using ReflectionExtensions.Reflection.Emit;

//namespace Obscuro.Pipeline.Packing.Outputs.Internal
//{
//    class ObscuroAssemblyGenerator
//    {
//        public static Assembly GenerateAssembly(IObscuroContext context,
//                                                Dictionary<string, Stream> libraries)
//        {
//            var outputFileName = context.FileName;
//            var version = typeof(ObscuroAssemblyGenerator).Assembly.GetName().Version;

//            var libName = new AssemblyName();
//            libName.Name = Path.GetFileNameWithoutExtension(outputFileName);
//            libName.Version = version;

//            var assemblyDir = Path.GetDirectoryName(outputFileName);
//            var assemblyBuilder = CreateAssembly(libName, assemblyDir);
//            var moduleBuilder = assemblyBuilder.DefineDynamicModule(outputFileName);

//            var typeBuilder = moduleBuilder.DefineType("AppLauncher", TypeAttributes.Public
//                                                                    | TypeAttributes.Class
//                                                                    | TypeAttributes.BeforeFieldInit, typeof(object));

//            var mainBuilder = typeBuilder.DefineMethod("Main",
//                                                MethodAttributes.Public | MethodAttributes.Static,
//                                                CallingConventions.Standard,
//                                                typeof(void),
//                                                new[] { typeof(string[]) });

//            mainBuilder.DefineParameter(1, ParameterAttributes.None, "args");

//            var launcherType = typeof(ObscuroLauncher);

//            var mainEmitter = new EmitHelper(mainBuilder.GetILGenerator());

//            var appLauncher = mainEmitter.DeclareLocal(launcherType);

//            mainEmitter.nop
//                       .newobj(launcherType.GetConstructor(Type.EmptyTypes))
//                       .stloc(appLauncher)
//                       .ldloc(appLauncher)
//                       .ldstr(context.StartInfo.EntryAssemblyName);

//            if (context.Key == null)
//                mainEmitter.ILGenerator.Emit(OpCodes.Ldnull);
//            else
//                mainEmitter.ldstr(context.Key);

//            // NAIL: need to pass context.Settings
//            mainEmitter.ldnull
//                       .callvirt(launcherType.GetMethod("Run"))
//                       .ret();

//            typeBuilder.CreateType();

//            // Add packed libraries to the output lib resource
//            foreach (var resourcePair in libraries)
//                moduleBuilder.DefineManifestResource(resourcePair.Key,
//                                                     resourcePair.Value,
//                                                     ResourceAttributes.Public);

//            assemblyBuilder.SetEntryPoint(mainBuilder);
//            assemblyBuilder.Save(outputFileName);

//            return assemblyBuilder;
//        }

//        private static AssemblyBuilder CreateAssembly(AssemblyName libName, string assemblyDir)
//        {
//            if (string.IsNullOrEmpty(assemblyDir))
//                return AssemblyBuilder.DefineDynamicAssembly(libName, AssemblyBuilderAccess.RunAndSave);

//            return AssemblyBuilder.DefineDynamicAssembly(libName, AssemblyBuilderAccess.RunAndSave, assemblyDir);
//        }
//    }
//}

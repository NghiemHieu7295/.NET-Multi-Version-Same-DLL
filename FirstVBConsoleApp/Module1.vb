Imports System.IO
Imports System.Reflection

Module Module1

    Sub Main()
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssembly

        'Continue main program
    End Sub

    Private Function ResolveAssembly(sender As Object, args As ResolveEventArgs) As Assembly
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim libPaths As String() = {Path.Combine(basePath, "thirdparties", "newtonsoft", "v1"), Path.Combine(basePath, "thirdparties", "newtonsoft", "v2")}

        For Each libPath In libPaths
            If Directory.Exists(libPath) Then
                Dim dllPath As String = Path.Combine(libPath, New AssemblyName(args.Name).Name & ".dll")
                If File.Exists(dllPath) Then
                    Return Assembly.LoadFrom(dllPath)
                End If
            End If
        Next

        Return Nothing
    End Function

End Module

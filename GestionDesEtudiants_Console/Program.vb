'Yahya Lazrek
'Group : 2APG2
'Gestion des etudiants

Module Program

    Dim etudiants() As Etudiant
    Dim etudiantCounter As Integer = 0

    Sub Main()

        While True

            Select Case Menu()
                Case 1
                    Console.Clear()
                    AjouterEtudiant()
                Case 2
                    Console.Clear()
                    AjouterEtudiants()
                Case 3
                    Console.Clear()
                    ModifierEtudiant()
                Case 4
                    Console.Clear()
                    SupprimerEtudiant()
                Case 5
                    Console.Clear()
                    AfficherEtudiants()
                Case 6
                    End
            End Select

            Console.WriteLine(vbCrLf + "Taper pour revenir a la menu")
            Console.ReadKey()
        End While

    End Sub

    Function Menu() As Integer
        Dim choix As Integer

        Console.Clear()

        Console.WriteLine("Gestion Des Etudiants")
        Console.WriteLine("Taper 1 pour ajouter un etudiant")
        Console.WriteLine("Taper 2 pour ajouter plusieurs etudiants")
        Console.WriteLine("Taper 3 pour modifier un etudiant")
        Console.WriteLine("Taper 4 pour supprimer un etudiant")
        Console.WriteLine("Taper 5 pour afficher la liste des etudiants")
        Console.WriteLine("Taper 6 pour quitter le programme")

        Do
            Try
                Console.Write("> ")
                choix = Console.ReadLine()
            Catch ex As Exception
                choix = -1
            End Try
        Loop While choix < 1 Or choix > 6

        Return choix
    End Function

    Sub AjouterEtudiant()

        etudiantCounter += 1
        ReDim Preserve etudiants(etudiantCounter)

        etudiants(etudiantCounter - 1) = New Etudiant()

        InsertInformation(etudiantCounter - 1)

    End Sub
    Sub AjouterEtudiants()
        Dim n As Integer

        Console.Write("Donnez le nombre des etudiants a ajouter : ")
        n = Console.ReadLine()

        For i = 1 To n
            AjouterEtudiant()
        Next
    End Sub
    Sub ModifierEtudiant()
        Dim index As Integer

        If etudiantCounter = 0 Then
            Console.WriteLine("il n'y a pas d'�tudiants")
            Exit Sub
        End If
        Do
            Console.Write("Donnez l'indice d'etudiant : ")
            index = Console.ReadLine()
        Loop While index < 0 Or index >= etudiantCounter

        InsertInformation(index)

    End Sub
    Sub SupprimerEtudiant()
        Dim index As Integer

        If etudiantCounter = 0 Then
            Console.WriteLine("il n'y a pas d'�tudiants")
            Exit Sub
        End If
        Do
            Console.Write("Donnez l'indice d'etudiant : ")
            index = Console.ReadLine()
        Loop While index < 0 Or index >= etudiantCounter

        For i = index To etudiantCounter - 2
            etudiants(i) = etudiants(i + 1)
        Next

        etudiantCounter -= 1
        ReDim Preserve etudiants(etudiantCounter)

    End Sub
    Sub AfficherEtudiants()

        If etudiantCounter = 0 Then
            Console.WriteLine("il n'y a pas d'�tudiants")
            Exit Sub
        End If

        For i = 0 To etudiantCounter - 1
            Console.WriteLine("Etudiant " & i + 1)
            Console.WriteLine(" nom      : " + etudiants(i).GetNom())
            Console.WriteLine(" Notes")
            Console.WriteLine("  note 1  : " + etudiants(i).GetNotes()(0).ToString())
            Console.WriteLine("  note 2  : " + etudiants(i).GetNotes()(1).ToString())
            Console.WriteLine("  note 3  : " + etudiants(i).GetNotes()(2).ToString())
            Console.WriteLine("  moyenne : " + etudiants(i).GetNotes()(3).ToString())
            Console.WriteLine(" Mention  : " + etudiants(i).GetResultat()(1))
            Console.WriteLine(" Resultat : " + etudiants(i).GetResultat()(0))
            Console.WriteLine()
        Next

    End Sub
    Sub InsertInformation(index As Integer)
        Dim nom As String
        Dim notes(4) As Double
        Dim resultat(2) As String

        'nom d'etudiant
        Do
            Console.Write("Donnez nom d'etudiant " & (index + 1) & " : ")
            nom = Console.ReadLine()

            If String.IsNullOrWhiteSpace(nom) Then
                Console.WriteLine("le nom ne doit pas �tre un espace vide ou blanc")
            Else
                Exit Do
            End If
        Loop While True
        etudiants(index).SetNom(nom)

        'notes d'etudiant
        Console.WriteLine("Donnez les notes : ")
        notes(0) = GetNote(" Notes 1 : ")
        notes(1) = GetNote(" Notes 2 : ")
        notes(2) = GetNote(" Notes 3 : ")
        'Calculer la moyenne
        notes(3) = (notes(0) + notes(1) + notes(2)) / 3
        etudiants(index).SetNotes(notes)

        'resultat d'etudiant
        If notes(3) >= 10 Then
            resultat(0) = "admis"
        Else
            resultat(0) = "non admis"
        End If
        'mention
        If notes(3) >= 16 Then
            resultat(1) = "tres bien"
        ElseIf notes(3) >= 14 Then
            resultat(1) = "bien"
        ElseIf notes(3) >= 12 Then
            resultat(1) = "assez-bien"
        ElseIf notes(3) >= 10 Then
            resultat(1) = "passable"
        Else
            resultat(1) = "redoublant"
        End If
        etudiants(index).SetResultat(resultat)
    End Sub
    Function GetNote(text As String) As Double
        Dim note As Double
        Do
            Try
                Console.Write(text)
                note = Console.ReadLine()
            Catch ex As Exception
                Console.WriteLine("erreur de saisie")
                Continue Do
            End Try

            If note < 0 Or note > 20 Then
                Console.WriteLine("la note doit etre entre 0 et 20")
            Else
                Exit Do
            End If

        Loop While True

        Return note
    End Function
End Module

Class Etudiant
    Dim nom As String
    Dim notes(4) As Double
    Dim resultat(2) As String

    'Constructors
    Public Sub New()
    End Sub
    Public Sub New(nom As String)
        Me.nom = nom
    End Sub
    Public Sub New(nom As String, notes() As Double)
        Me.nom = nom
        Me.notes = notes
    End Sub
    Public Sub New(nom As String, notes() As Double, resultat() As String)
        Me.nom = nom
        Me.notes = notes
        Me.resultat = resultat
    End Sub

    'Nom
    Function GetNom()
        Return nom
    End Function
    Sub SetNom(nom As String)
        Me.nom = nom
    End Sub
    'Notes et moyenne
    Function GetNotes()
        Return notes
    End Function
    Sub SetNotes(notes() As Double)
        Me.notes = notes
    End Sub
    'Resultat
    Function GetResultat()
        Return resultat
    End Function
    Sub SetResultat(resultat() As String)
        Me.resultat = resultat
    End Sub

End Class
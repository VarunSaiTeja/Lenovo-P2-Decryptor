Public Class decryptor
    Dim state As String
    Public states As Integer

    Public Sub check()
        Dim check As New Process
        check.StartInfo.FileName = "adb.exe"
        check.StartInfo.Arguments = "get-state"
        check.StartInfo.UseShellExecute = False
        check.StartInfo.CreateNoWindow = True
        check.StartInfo.RedirectStandardOutput = True
        check.Start()
        state = check.StandardOutput.ReadToEnd
        check.WaitForExit()

        states = "0"

        If String.Compare(state, "device", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "1"
        End If

        If String.Compare(state, "recovery", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "3"
        End If

        If String.Compare(state, "sideload", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "4"
        End If

        If states = "0" Then
            Dim fcheck As New Process
            fcheck.StartInfo.FileName = "fastboot.exe"
            fcheck.StartInfo.Arguments = "devices"
            fcheck.StartInfo.UseShellExecute = False
            fcheck.StartInfo.CreateNoWindow = True
            fcheck.StartInfo.RedirectStandardOutput = True
            fcheck.Start()
            state = fcheck.StandardOutput.ReadToEnd
            fcheck.WaitForExit()

            If String.Compare(state, "fastboot", StringComparison.InvariantCultureIgnoreCase) = 1 Then
                states = "2"
            End If

        End If

    End Sub

    Private Sub decryptor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox2.Select()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("drivers.exe")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        check()
        If states = "0" Then
            MsgBox("Device Not Connected")
        End If

        If states = "1" Or states = "3" Then
            Dim reboot As New Process
            reboot.StartInfo.FileName = "adb.exe"
            reboot.StartInfo.Arguments = "reboot bootloader"
            reboot.StartInfo.UseShellExecute = False
            reboot.StartInfo.CreateNoWindow = True
            reboot.Start()
            reboot.WaitForExit()
            Threading.Thread.Sleep(10000)
            check()
            If states = "0" Then
                MsgBox("Device Not Detected" & vbNewLine& & vbNewLine& + "Install Lenovo Drivers")
            End If
        End If

        If states = "2" Then
            Dim decrypt As New Process
            decrypt.StartInfo.FileName = "fastboot.exe"
            decrypt.StartInfo.Arguments = "flash userdata userdata.img"
            decrypt.StartInfo.UseShellExecute = False
            decrypt.StartInfo.CreateNoWindow = True
            decrypt.Start()
            decrypt.WaitForExit()
            decrypt.StartInfo.Arguments = "reboot"
            decrypt.Start()
            decrypt.WaitForExit()
            MsgBox("Device Decrypted Successfully")
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("https://api.whatsapp.com/send?phone=919010075670")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Process.Start("https://www.youtube.com/channel/UCyq7mspndOnlqR41axhhT8A")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start("https://www.paypal.me/varunsaiteja")
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Process.Start("http://tiny.cc/varunpaytm")
    End Sub
End Class

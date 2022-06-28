//////////////////////////////////////
//     R.I.T.A - The Assistant      //
//////////////////////////////////////
// Created By Paulo Gustavo Lapetina//
//////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Xml;
using System.IO;


namespace RITA
{
    public partial class Main : Form
    {
        #region definition tools
        SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        Boolean wake = false;
        Boolean wake2 = false;

        String logpath = @"C:\Rita Logs\log.txt";
        String path = @"C:\Rita Logs";
        String namefile = @"C:\Rita Logs\name.txt";

        String temp;
        String condition;

        int Fx;

        Choices list = new Choices();
        #endregion

        public Main()
        {
            SpeechRecognitionEngine _speechRecognitionEngine = new SpeechRecognitionEngine();

            list.Add(new String[] {
                "olá",
                "como vai você",
                "que horas são",
                "que dia é hoje",
                "como está o tempo",
                "abrir google",
                "dormir",
                "acordar",
                "abrir",
                "fechar",
                "reiniciar",
                "atualizar",
                "desligar",
                "abrir youtube",
                "qual a temperatura atual",
                "ola rita",
                "play",
                "pause",
                "qual meu nome",
                "abrir spotfy" });

            Grammar _grammar = new Grammar(new GrammarBuilder(list));

            try
            {
                _speechRecognitionEngine.RequestRecognizerUpdate();
                _speechRecognitionEngine.LoadGrammar(_grammar);
                _speechRecognitionEngine.SpeechRecognized += _speechRecognitionEngine_SpeachRecognized;
                _speechRecognitionEngine.SetInputToDefaultAudioDevice();
                _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }

            _speechSynthesizer.SelectVoiceByHints(VoiceGender.Female);

            InitializeComponent();
        }
        public void restart()
        {
            Process.Start(@"C:\Voice Bot\Voice Bot.exe");
            Environment.Exit(0);
        }

        public void say(String h)
        {
            _speechSynthesizer.SpeakAsync(h);
            wake2 = false;
        }

        private void _speechRecognitionEngine_SpeachRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            #region Sleep/Wake

            if (r == "olá rita")
            {
                wake2 = true;
            }

            if (r == "reiniciar" || r == "atualizar")
            {
                restart();
            }

            if (r == "acordar")
            {
                say("modo de dormir desligado.");
                wake = true;
                //label3.Text = "State: Awake";
            }
            if (r == "dormir")
            {
                say("entrando no modo de dormir");
                wake = false;
                //label3.Text = "State: Sleep";
            }
            #endregion

            if (wake == true || wake2 == true)
            {
                #region Commands

                //what you say
                if (r == "olá")
                {
                    //what it says
                    say("olá");
                }

                if (r == "que horas são" || r == "como esta o tempo")
                {
                    say(DateTime.Now.ToString("h:mm tt"));
                }

                if (r == "que dia é hoje")
                {
                    say(DateTime.Now.ToString("M/d/yyyy"));
                }

                if (r == "abrir google")
                {
                    Process.Start("https://google.rs");
                    say("vai dar uma googlada né safado");
                }

                if (r == "abrir youtube")
                {
                    Process.Start("https://www.youtube.com");
                    say("abrindo youtube");
                }

                if (r == "como está você?")
                {
                    say("Muito bem, eu sou a porra de uma máquina... e você?");
                }

                if (r == "como está o tempo")
                {
                    say("meu mestre ainda não implementou essa função");
                }

                if (r == "qual a temperatura")
                {
                    //Int32.TryParse(GetWeather("temp"), out Fx);
                    //double Cx = 5.0 / 9.0 * (Fx - 32);
                    say("Se meu pai não implementou o tempo, imagine a temperatura?");
                }

                if (r == "play" || r == "pause")
                {
                    SendKeys.Send(" ");
                }

                if (r == "qual meu nome")
                {
                    say(File.ReadAllText(namefile));
                }
                if (r == "abrir spotfy")
                {
                    //string winpath = Environment.GetEnvironmentVariable("spotfy.exe");
                    //string path = System.IO.Path.GetDirectoryName(
                    //              System.Windows.Forms.Application.ExecutablePath);

                    //Process.Start(winpath + @"\Microsoft.NET\Framework\v1.0.3705\Installutil.exe",
                    //path + "\\MyService.exe");

                    say("meu pai ainda não implementou isso");
                }
            }
            if (r == "Desligar")
            {
                say("Adeus.");
                System.Threading.Thread.Sleep(1500);
                Environment.Exit(0);
            }
            //textBox1.AppendText(r + "\n");
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;

namespace DialogueSystem
{ 
    public interface IDialogueLine
    {
        void OnExecute(DialogueMachine pMachine);
    }

    public class DialogueTalkLine : IDialogueLine
    {
        private String m_talker;
        private String m_line;

        public DialogueTalkLine(String pTalker, String pLine)
        {
            m_talker = pTalker;
            m_line = pLine;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WritePos(m_talker);
            pMachine.Output.WriteLine(m_line);

            pMachine.Output.DoPrint(pMachine.NextLine);
            //얘가 프린팅 다 끝나면 너가 알아서 다음 줄 실행해라
        }
    }

    public class DialogueSelectLine : IDialogueLine
    {
        private String m_talker;
        private String m_line;
        private String[] m_selects;

        public DialogueSelectLine(String pTalker, String pLine, String[] pSelects)
        {
            m_talker = pTalker;
            m_line = pLine;
            m_selects = pSelects;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WritePos(m_talker);
            pMachine.Output.WriteLine(m_line);
            pMachine.Output.WriteSelections(m_selects);

            pMachine.Output.DoPrint(pMachine.NextLine);
        }
    }

    public class IntInput
    {
        public Int32 Value { get; set; }
    }

    public class DialogueInputHandleLine : IDialogueLine
    {
        private IntInput m_input;

        public DialogueInputHandleLine(IntInput pInput)
        {
            m_input = pInput;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            m_input.Value = pMachine.Input.ReadSelection();
            pMachine.NextLine();
        }
    }

    public interface IDialogueOutput
    {
        void WritePos(String pPos);
        void WriteLine(String pLine);
        void WriteSelections(String[] pSelections);

        void BeginPrint();
        void DoPrint(Action pNext);
        void EndPrint();
    }

    public interface IDialogueInput
    {
        Int32 ReadSelection();
    }


    public class DialogueMachine
    {
        private IDialogueInput m_input;
        private IDialogueOutput m_output;

        public IDialogueInput Input => m_input;
        public IDialogueOutput Output => m_output;

        public void BindInput(IDialogueInput pInput)
        {
            m_input = pInput;
        }
        public void BindOutput(IDialogueOutput pOutput)
        {
            m_output = pOutput;
        }

        private IEnumerator<IDialogueLine> m_enumerator;
        public void RunDialog(IEnumerator<IDialogueLine> pLineEnumerator)
        {
            m_enumerator = pLineEnumerator;
            m_output.BeginPrint();
            NextLine();
        }

        public void NextLine()
        {
            if (m_enumerator.MoveNext())
            {
                m_enumerator.Current.OnExecute(this);
            }
            else
            {
                m_output.EndPrint();
            }
        }

    }

}
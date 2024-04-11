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
            pMachine.Output.WriteTalker(m_talker);
            pMachine.Output.WriteLine(m_line);
            pMachine.Output.WriteKey(-1);

            pMachine.Output.DoPrint(pMachine.NextLine);
            //얘가 프린팅 다 끝나면 너가 알아서 다음 줄 실행해라
        }
    }

    public class DialogueSelectLine : IDialogueLine
    {
        private String m_talker;
        private String[] m_selects;

        public DialogueSelectLine(String pTalker, String[] pSelects)
        {
            m_talker = pTalker;
            m_selects = pSelects;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WriteTalker(m_talker);
            pMachine.Output.WriteSelections(m_selects);

            pMachine.Output.DoPrint(pMachine.NextLine);
        }
    }

    public class DialogueKeyInputLine : IDialogueLine
    {
        private String m_talker;
        private String m_line;
        private Int32 m_key;

        public DialogueKeyInputLine(String pTalker, String pLine, Int32 pKey)
        {
            m_talker = pTalker;
            m_line = pLine;
            m_key = pKey;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WriteTalker(m_talker);
            pMachine.Output.WriteLine(m_line);
            pMachine.Output.WriteKey(m_key);

            pMachine.Output.DoPrint(pMachine.NextLine);
        }
    }

    public class DialogueActionLine : IDialogueLine
    {
        private String m_action;

        public DialogueActionLine(String pAction)
        {
            m_action = pAction;
        }

        public void OnExecute(DialogueMachine pMachine)
        {
            pMachine.Output.WriteLine(m_action);

            pMachine.Output.DoPrint(m_action, pMachine.NextLine);
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
            m_input.Value = pMachine.Input.ReadInput();
            pMachine.NextLine();
        }
    }

    public interface IDialogueOutput
    {
        void WriteTalker(String pTalker);
        void WriteLine(String pLine);
        void WriteSelections(String[] pSelections);
        void WriteKey(Int32 pKey);

        void BeginPrint();
        void DoPrint(Action pNext);
        void DoPrint(String pAction, Action pNext);
        void EndPrint();
    }

    public interface IDialogueInput
    {
        Int32 ReadInput();
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
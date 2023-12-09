using System.CodeDom;
using System.Drawing.Printing;

namespace ChemReactionsCalc
{
    public partial class Form1 : Form
    {
        //������� �������� � ������� ������� ��������������. ��������� ��� �������� �����.
        readonly Ion[] _anions;
        readonly Ion[] _cations;

        //����� � ������, ������ ����������. ��������� ��� ������ �� ������. ������� ��� null.
        Ion? _anion1;
        Ion? _cation1;
        private readonly Compound _compound1 = new();

        //����� � ������, ������ ����������. ��������� ��� ������ �� ������. ������� ��� null.
        Ion? _anion2;
        Ion? _cation2;
        private readonly Compound _compound2 = new();

        //������ � ���������� ����������, ����������� ������� ������ �� ������ IonicReactionEquation
        private readonly Compound _compound3 = new();
        private readonly Compound _compound4 = new();

        //��������� ������� � ������� ����������, ������������ ������ ������, ��� ������������ ������ ����������.
        private string? _currentCompound1;
        private string? _currentCompound2;

        private bool _isStartCompounsReady = false;
        private bool _isFinalCompoundsGood = false;

        private readonly int _shiftReactionLabelPixelsCount = 5;

        //���������. ����������� �� �������, ���������
        private readonly ToolTip _tip = new();

        public Form1()
        {
            InitializeComponent();

            //�������� ������� ������ � ��������
            _anions = SolubilityTable.GetAnions();
            _cations = SolubilityTable.GetCations();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // �������� ������� ������� �� ��������.
            // � �������� ������ ����� ������������ ����� Ion.GetToString(), ������� ��������� ��������� ��������� ��������� ����. 
            foreach (Ion anion in _anions)
            {
                AnionListBox1.Items.Add(anion.GetToString());
            }

            foreach (Ion cation in _cations)
            {
                CationListBox1.Items.Add(cation.GetToString());
            }

            foreach (Ion anion in _anions)
            {
                AnionListBox2.Items.Add(anion.GetToString());
            }

            foreach (Ion cation in _cations)
            {
                CationListBox2.Items.Add(cation.GetToString());
            }

            //�������� ������, �������� ����������.
            SetStartText();

            //��������������� ���������.
            SetButtonReadyReactTip();
            _tip.SetToolTip(QuestionButton, "����������");
            _tip.SetToolTip(ReactionLabel, "�������, ��� �� ����������� � ����� ������.");
        }

        private void SetButtonReadyReactTip()
        {
            if (_isStartCompounsReady)
            {
                if (_isFinalCompoundsGood)
                {
                    _tip.SetToolTip(StartReactionButton, "�������! ������� ������� �������.");
                }
                else
                {
                    _tip.SetToolTip(StartReactionButton, "������� ���������. ������������ ���������� ����� �����������.");
                }
            }
            else {
                _tip.SetToolTip(StartReactionButton, "����� ��������, ����� ��������� ���������� ����������");
            }            
        }

        /// <summary>
        /// ������� label-� � ��������� ��������� ������ TexBox
        /// </summary>
        private void SetStartText()
        {
            //������� ������ � ������
            ClearLabel(ReactionLabel);
            ClearLabel(CompoundLabel1);
            ClearLabel(CompoundLabel2);
            ClearLabel(SolubilityLabel1);
            ClearLabel(SolubilityLabel2);

            //� ��������� ���� ������� ����������
            PrintManual();
        }

        /// <summary>
        /// ��������� ���������� � ���������.
        /// </summary>
        private void PrintManual()
        {
            MessageTextBox.Text = Message.Manual;
            MessageTextBox.ForeColor = Color.Black;
        }

        /// <summary>
        /// ������� ����
        /// </summary>
        private void ClearMessageBox()
        {
            MessageTextBox.Text = "";
        }


        private void SetStartReactionLabelText()
        {
            ReactionLabel.Text = _currentCompound1 + _currentCompound2;
        }

        private void PrintCompand1(int multiplier)
        {
            if (_currentCompound1 == null)
            {
                _currentCompound1 = "";
            }

            CompoundLabel1.Text = multiplier == 1 ?
                _compound1.Show(false) :
                multiplier.ToString() + _compound1.Show(false);

            SetCurrentCompound(1, multiplier, _compound1, ref _currentCompound1);

            SetStartReactionLabelText();
        }

        private static void SetCurrentCompound(int compoundIndex, int multiplier, Compound compound, ref string currentCompound)
        {
            currentCompound = multiplier == 1 ?
                compound.Show(false) :
                multiplier.ToString() + compound.Show(false);

            if (compoundIndex == 2)
            {
                currentCompound = " + " + currentCompound;
            }
        }

        private void PrintCompand2(int multiplier)
        {
            if (_currentCompound2 == null)
            {
                _currentCompound2 = "";
            }

            CompoundLabel2.Text = multiplier == 1 ?
                _compound2.Show(false) :
                multiplier.ToString() + _compound2.Show(false);

            SetCurrentCompound(2, multiplier, _compound2, ref _currentCompound2);

            SetStartReactionLabelText();
        }

        private void AnionListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearMessageBox();

            _anion1 = _anions[AnionListBox1.SelectedIndex];
            _compound1.AddIon(_anion1, false);

            PrintCompand1(1);

            ShowCompaundSolubility(SolubilityLabel1, _compound1);

            //��������� ���������� � �������, ��������� ���������� ��� ������� ����� ������ ������ � ��� �������� ��������� � �������� ���������� � �������� �� ���� ����������.
            CheckReadyReacAndSetColors();
        }

        private void CheckReadyReacAndSetColors()
        {
            _isStartCompounsReady = AreCompoundsReadyToReactAndPrintMessage();
            SetStartReactionButtonColor(_isStartCompounsReady, _compound1, _compound2);
            SetButtonReadyReactTip();
        }

        private void SetStartReactionButtonColor(bool isStartCompounsReady, Compound compound1, Compound compound2)
        {
            if (isStartCompounsReady)
            {
                _isFinalCompoundsGood = IonicReactionEquation.IsEquationWorking(compound1, compound2, out _, out _);//��������� ��� �������, ������ "_".
                StartReactionButton.ForeColor = _isFinalCompoundsGood ? Color.Yellow : Color.White;
                StartReactionButton.BackColor = _isFinalCompoundsGood ? Color.DarkGreen : Color.Black;

            }
            else
            {
                StartReactionButton.ForeColor = Color.Black;
                StartReactionButton.BackColor = SystemColors.ControlLight;
            }
        }

        private void CationListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearMessageBox();

            _cation1 = _cations[CationListBox1.SelectedIndex];
            _compound1.AddIon(_cation1, true);

            PrintCompand1(1);

            ShowCompaundSolubility(SolubilityLabel1, _compound1);

            //��������� ���������� � �������, ��������� ���������� ��� ������� ����� ������ ������ � ��� �������� ��������� � �������� ���������� � �������� �� ���� ����������.
            CheckReadyReacAndSetColors();
        }

        private bool AreCompoundsReadyToReactAndPrintMessage()
        {
            bool result = IonicReactionEquation.AreCompoundsReadyToReact(_compound1, _compound2, out string message);

            MessageTextBox.Text = message;
            MessageTextBox.ForeColor = result ? Color.Green : Color.Red;

            return result;
        }

        private static void ClearLabel(Label label)
        {
            label.Text = "";
        }

        private static void ShowCompaundSolubility(Label label, Compound compound)
        {
            string? currentSolubility = IonicReactionEquation.ShowCompaundSolubilityMessage(compound); ;
            label.Text = currentSolubility == null ? "" : currentSolubility;
        }

        private void CationListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearMessageBox();

            _cation2 = _cations[CationListBox2.SelectedIndex];
            _compound2.AddIon(_cation2, true);

            PrintCompand2(1);

            ShowCompaundSolubility(SolubilityLabel2, _compound2);

            //��������� ���������� � �������, ��������� ���������� ��� ������� ����� ������ ������ � ��� �������� ��������� � �������� ���������� � �������� �� ���� ����������.
            CheckReadyReacAndSetColors();
        }

        private void AnionListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearMessageBox();

            _anion2 = _anions[AnionListBox2.SelectedIndex];
            _compound2.AddIon(_anion2, false);

            PrintCompand2(1);

            ShowCompaundSolubility(SolubilityLabel2, _compound2);

            //��������� ���������� � �������, ��������� ���������� ��� ������� ����� ������ ������ � ��� �������� ��������� � �������� ���������� � �������� �� ���� ����������.
            CheckReadyReacAndSetColors();
        }

        private void StartReactionButton_Click(object sender, EventArgs e)
        {
            // ��������� ���������� �� ����������� ����������. ���� ���, ������� �� ������.
            if (!AreCompoundsReadyToReactAndPrintMessage())
            {
                return;
            }

            // �������� �� ������� ��������� ������ � �������. �������� ��������������, ����� ��� �� VS �� ��������.
            if (_cation1 == null || _anion1 == null || _cation2 == null || _anion2 == null)
            {
                return;
            }

            // ��������� �������� ����������, ����� ������ ������� �������.
            _compound3.AddAllIons(_cation1, _anion2);
            _compound4.AddAllIons(_cation2, _anion1);

            // ��������� ����� ������� ������� ������, ����� ���������� ��� bool ��� �������� ��� ������� ����� ������ � ���������.
            SetTextBoxColor(MessageTextBox, IonicReactionEquation.IsEquationWorking(_compound1, _compound2, out string ionExchangeEquation, out string �omment));
            ReactionLabel.Text = ionExchangeEquation;
            MessageTextBox.Text = �omment;
        }

        /// <summary>
        /// ������ ���� ������ � ��������� �����. ���� ���������� ����������� ��������, ���� ������ ����� �������, ����� - �������.
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="isEquationWorking"></param>
        private static void SetTextBoxColor(TextBox textBox, bool isEquationWorking)
        {
            textBox.ForeColor = isEquationWorking ? Color.Green : Color.Red;
        }

        private void QuestionButton_Click(object sender, EventArgs e)
        {
            PrintManual();
        }

        private void ReactionLabel_MouseDown(object sender, EventArgs e)
        {
            Clipboard.SetText(ReactionLabel.Text);

            ReactionLabel.Top += _shiftReactionLabelPixelsCount;
        }

        private void ReactionLabel_MouseUp(object sender, EventArgs e)
        {
            ReactionLabel.Top -= _shiftReactionLabelPixelsCount;
        }
    }
}
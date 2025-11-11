using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Test
{
    public partial class Form1 : Form
    {
        // constをつけると初期化時にのみ値の変更が可能になる
        
        /// <summary>
        /// ボタンの横幅
        /// </summary>
        const int BUTTON_SIZE_X = 100;
        /// <summary>
        /// ボタンの縦
        /// </summary>
        const int BUTTON_SIZE_Y = 100;

        /// <summary>
        /// ボタンが横に何個並ぶか
        /// </summary>
        const int BOARD_SIZE_X = 3;
        /// <summary>
        /// ボタンが縦に何個並ぶか
        /// </summary>
        const int BOARD_SIZE_Y = 3;

        /// <summary>
        /// TestButtonの二次元配列
        /// </summary>
        private TestButton[,] _buttonArray;
        
        public Form1()
        {
            InitializeComponent();
            
            //_buttonArrayの初期化
            _buttonArray = new TestButton[BOARD_SIZE_Y, BOARD_SIZE_X];

            for (int i = 0; i < BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < BOARD_SIZE_Y; j++)
                {
                    // インスタンスの生成
                    TestButton testButton =
                        new TestButton(
                            this,
                            i, j,
                            new Size(BUTTON_SIZE_X, BUTTON_SIZE_Y),
                            "ひのまる");

                    // 配列にボタンの参照を追加
                    _buttonArray[j, i] = testButton;

                    // コントロールにボタンを追加
                    Controls.Add(testButton);
                }
            }
        }
        /// <summary>
        /// TestButtonを取得する関数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TestButton GetTestButton(int x, int y)
        {
            // 配列外参照対策
            if (x < 0 || x >= BOARD_SIZE_X) return null;
            if (y < 0 || y >= BOARD_SIZE_Y) return null;

            return _buttonArray[y, x];
        }

        // 自動生成
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("くっりく");
        }

    
         private Random _rand = new Random(); // ← ランダム生成用

        

         private void Form1_Load(object sender, EventArgs e)
         {
                Random(); // ← 起動時にランダム化！
        　}

            /// <summary>
            /// ランダムに「解ける」盤面を生成
            /// </summary>
            private void Random()
            {
                // まず全OFFに
                foreach (var btn in _buttonArray)
                {
                    btn.SetEnable(false);
                }

                // ランダムにボタンを押す（クリック動作を再利用）
                int pressCount = _rand.Next(5, 10); // 5〜10回くらい押す
                for (int i = 0; i < pressCount; i++)
                {
                    int x = _rand.Next(BOARD_SIZE_X);
                    int y = _rand.Next(BOARD_SIZE_Y);
                    _buttonArray[y, x].PerformClick(); 
                }
            }

        //クリア表示
        public void CheckClear()
        {
            bool anyOn = false;   // 1つでもONがあるか
            bool anyOff = false;  // 1つでもOFFがあるか

            foreach (var btn in _buttonArray)
            {
                if (btn.IsOn) anyOn = true;
                else anyOff = true;

                if (anyOn && anyOff) return;
            }

            if (!anyOn && anyOff == true) //すべて赤
            {
                MessageBox.Show("クリアだね。もう一回遊べるドン！！");
                Random(); // 次のステージを開始（任意）
            }
            else if (!anyOff && anyOn == true) //すべて白
            {
                MessageBox.Show("そろえる色逆だよ！");
                
            }
            
        }

    }

}

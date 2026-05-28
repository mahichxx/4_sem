namespace lab_2_Airport
{
    partial class listCrew
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.rbPassenger = new System.Windows.Forms.RadioButton();
            this.rbCargo = new System.Windows.Forms.RadioButton();
            this.rbMilitary = new System.Windows.Forms.RadioButton();
            this.mtbYear = new System.Windows.Forms.MaskedTextBox();
            this.numSeats = new System.Windows.Forms.NumericUpDown();
            this.text1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackPayload = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpLastMaintenance = new System.Windows.Forms.DateTimePicker();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numManufYear = new System.Windows.Forms.NumericUpDown();
            this.txtManufCountry = new System.Windows.Forms.TextBox();
            this.txtManufName = new System.Windows.Forms.TextBox();
            this.txtCrewName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbCrewPost = new System.Windows.Forms.ComboBox();
            this.numCrewAge = new System.Windows.Forms.NumericUpDown();
            this.btnAddCrewMember = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPayload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManufYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCrewAge)).BeginInit();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(124, 116);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(117, 22);
            this.txtID.TabIndex = 0;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // cmbModel
            // 
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Items.AddRange(new object[] {
            "Airbus A320",
            "Boing 737",
            "MC-21"});
            this.cmbModel.Location = new System.Drawing.Point(126, 157);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(115, 24);
            this.cmbModel.TabIndex = 1;
            // 
            // rbPassenger
            // 
            this.rbPassenger.AutoSize = true;
            this.rbPassenger.Location = new System.Drawing.Point(41, 240);
            this.rbPassenger.Name = "rbPassenger";
            this.rbPassenger.Size = new System.Drawing.Size(123, 20);
            this.rbPassenger.TabIndex = 2;
            this.rbPassenger.TabStop = true;
            this.rbPassenger.Text = "Пассажирский";
            this.rbPassenger.UseVisualStyleBackColor = true;
            // 
            // rbCargo
            // 
            this.rbCargo.AutoSize = true;
            this.rbCargo.Location = new System.Drawing.Point(41, 266);
            this.rbCargo.Name = "rbCargo";
            this.rbCargo.Size = new System.Drawing.Size(91, 20);
            this.rbCargo.TabIndex = 3;
            this.rbCargo.TabStop = true;
            this.rbCargo.Text = "Грузовой";
            this.rbCargo.UseVisualStyleBackColor = true;
            // 
            // rbMilitary
            // 
            this.rbMilitary.AutoSize = true;
            this.rbMilitary.Location = new System.Drawing.Point(41, 292);
            this.rbMilitary.Name = "rbMilitary";
            this.rbMilitary.Size = new System.Drawing.Size(86, 20);
            this.rbMilitary.TabIndex = 4;
            this.rbMilitary.TabStop = true;
            this.rbMilitary.Text = "Военный";
            this.rbMilitary.UseVisualStyleBackColor = true;
            // 
            // mtbYear
            // 
            this.mtbYear.Location = new System.Drawing.Point(381, 119);
            this.mtbYear.Mask = "0000";
            this.mtbYear.Name = "mtbYear";
            this.mtbYear.Size = new System.Drawing.Size(115, 22);
            this.mtbYear.TabIndex = 5;
            // 
            // numSeats
            // 
            this.numSeats.Location = new System.Drawing.Point(382, 158);
            this.numSeats.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numSeats.Name = "numSeats";
            this.numSeats.Size = new System.Drawing.Size(114, 22);
            this.numSeats.TabIndex = 6;
            // 
            // text1
            // 
            this.text1.AutoSize = true;
            this.text1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text1.Location = new System.Drawing.Point(441, 9);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(153, 36);
            this.text1.TabIndex = 7;
            this.text1.Text = "Аэропорт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = " ID самолета";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Модель ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Тип самолета";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Год выпуска";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Количество мест";
            // 
            // trackPayload
            // 
            this.trackPayload.Location = new System.Drawing.Point(255, 252);
            this.trackPayload.Maximum = 200;
            this.trackPayload.Minimum = 20;
            this.trackPayload.Name = "trackPayload";
            this.trackPayload.Size = new System.Drawing.Size(241, 56);
            this.trackPayload.TabIndex = 13;
            this.trackPayload.Value = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Грузоподъемность ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(264, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "20";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(468, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "200";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 355);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(244, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Дата последнего тех. обслуживания";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 398);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(315, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "Эксплуатируется ли самолет в данный момент";
            // 
            // dtpLastMaintenance
            // 
            this.dtpLastMaintenance.Location = new System.Drawing.Point(316, 350);
            this.dtpLastMaintenance.Name = "dtpLastMaintenance";
            this.dtpLastMaintenance.Size = new System.Drawing.Size(180, 22);
            this.dtpLastMaintenance.TabIndex = 20;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(356, 398);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(46, 20);
            this.chkIsActive.TabIndex = 21;
            this.chkIsActive.Text = "Да";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(777, 160);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(180, 116);
            this.listBox1.TabIndex = 22;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(112, 446);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 35);
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(225, 446);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 35);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(112, 495);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 34);
            this.btnLoad.TabIndex = 25;
            this.btnLoad.Text = "Загрузка";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(225, 495);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(97, 34);
            this.btnCalculate.TabIndex = 26;
            this.btnCalculate.Text = "Подсчёт ";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(565, 336);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(190, 29);
            this.label13.TabIndex = 29;
            this.label13.Text = "Производитель";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(27, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 29);
            this.label14.TabIndex = 30;
            this.label14.Text = "Самолет";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(718, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 29);
            this.label15.TabIndex = 31;
            this.label15.Text = "Экипаж";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(803, 336);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 29);
            this.label12.TabIndex = 32;
            this.label12.Text = "Самолеты";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(556, 396);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 16);
            this.label16.TabIndex = 33;
            this.label16.Text = "Компании";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(556, 430);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 16);
            this.label17.TabIndex = 34;
            this.label17.Text = "Страна";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(556, 466);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(103, 16);
            this.label18.TabIndex = 35;
            this.label18.Text = "Год основания";
            // 
            // numManufYear
            // 
            this.numManufYear.Location = new System.Drawing.Point(662, 464);
            this.numManufYear.Maximum = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            this.numManufYear.Minimum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.numManufYear.Name = "numManufYear";
            this.numManufYear.Size = new System.Drawing.Size(93, 22);
            this.numManufYear.TabIndex = 36;
            this.numManufYear.Value = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            // 
            // txtManufCountry
            // 
            this.txtManufCountry.Location = new System.Drawing.Point(662, 427);
            this.txtManufCountry.Name = "txtManufCountry";
            this.txtManufCountry.Size = new System.Drawing.Size(93, 22);
            this.txtManufCountry.TabIndex = 37;
            // 
            // txtManufName
            // 
            this.txtManufName.Location = new System.Drawing.Point(662, 393);
            this.txtManufName.Name = "txtManufName";
            this.txtManufName.Size = new System.Drawing.Size(93, 22);
            this.txtManufName.TabIndex = 38;
            // 
            // txtCrewName
            // 
            this.txtCrewName.Location = new System.Drawing.Point(648, 127);
            this.txtCrewName.Name = "txtCrewName";
            this.txtCrewName.Size = new System.Drawing.Size(309, 22);
            this.txtCrewName.TabIndex = 39;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(556, 130);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 16);
            this.label19.TabIndex = 40;
            this.label19.Text = "ФИО";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(556, 170);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(81, 16);
            this.label20.TabIndex = 41;
            this.label20.Text = "Должность ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(556, 210);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 16);
            this.label21.TabIndex = 42;
            this.label21.Text = "Возраст";
            // 
            // cmbCrewPost
            // 
            this.cmbCrewPost.FormattingEnabled = true;
            this.cmbCrewPost.Items.AddRange(new object[] {
            "Стюардесса",
            "Штурман",
            "Пилот"});
            this.cmbCrewPost.Location = new System.Drawing.Point(648, 164);
            this.cmbCrewPost.Name = "cmbCrewPost";
            this.cmbCrewPost.Size = new System.Drawing.Size(107, 24);
            this.cmbCrewPost.TabIndex = 44;
            // 
            // numCrewAge
            // 
            this.numCrewAge.Location = new System.Drawing.Point(648, 210);
            this.numCrewAge.Name = "numCrewAge";
            this.numCrewAge.Size = new System.Drawing.Size(107, 22);
            this.numCrewAge.TabIndex = 45;
            // 
            // btnAddCrewMember
            // 
            this.btnAddCrewMember.Location = new System.Drawing.Point(564, 250);
            this.btnAddCrewMember.Name = "btnAddCrewMember";
            this.btnAddCrewMember.Size = new System.Drawing.Size(169, 23);
            this.btnAddCrewMember.TabIndex = 46;
            this.btnAddCrewMember.Text = "Добавить в список";
            this.btnAddCrewMember.UseVisualStyleBackColor = true;
            this.btnAddCrewMember.Click += new System.EventHandler(this.btnAddCrewMember_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(775, 388);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(182, 100);
            this.listBox2.TabIndex = 47;
            // 
            // listCrew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 554);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.btnAddCrewMember);
            this.Controls.Add(this.numCrewAge);
            this.Controls.Add(this.cmbCrewPost);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtCrewName);
            this.Controls.Add(this.txtManufName);
            this.Controls.Add(this.txtManufCountry);
            this.Controls.Add(this.numManufYear);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.dtpLastMaintenance);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackPayload);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.numSeats);
            this.Controls.Add(this.mtbYear);
            this.Controls.Add(this.rbMilitary);
            this.Controls.Add(this.rbCargo);
            this.Controls.Add(this.rbPassenger);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.txtID);
            this.Name = "listCrew";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPayload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManufYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCrewAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.RadioButton rbPassenger;
        private System.Windows.Forms.RadioButton rbCargo;
        private System.Windows.Forms.RadioButton rbMilitary;
        private System.Windows.Forms.MaskedTextBox mtbYear;
        private System.Windows.Forms.NumericUpDown numSeats;
        private System.Windows.Forms.Label text1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackPayload;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpLastMaintenance;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numManufYear;
        private System.Windows.Forms.TextBox txtManufCountry;
        private System.Windows.Forms.TextBox txtManufName;
        private System.Windows.Forms.TextBox txtCrewName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbCrewPost;
        private System.Windows.Forms.NumericUpDown numCrewAge;
        private System.Windows.Forms.Button btnAddCrewMember;
        private System.Windows.Forms.ListBox listBox2;
    }
}


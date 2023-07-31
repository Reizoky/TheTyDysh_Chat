using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheTyDysh
{
    public partial class frmInventory : Form
    {
        Character character;
        public frmInventory()
        {
            InitializeComponent();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            character = frmMain.frmMainWindow.character;
            LoadInfoStats();
            LoadInfoEquipment();
        }

        /// <summary>
        /// Загрузка статов
        /// </summary>
        private void LoadInfoStats()
        {
            lblStats.Text = "";
            character.GetStats();

            string strInfo = string.Empty;
            strInfo += $"Здоровье: {character.strength}";
            strInfo += Environment.NewLine + $"Сила({character.strength}): ";
            if (character.strength <= 20)
                strInfo += "Ты слаб, ты немощен, разве что палку и можешь держать.";
            else if (character.strength <= 30)
                strInfo += "Ты что то можешь, но не расчитывай, что горы свернуть сможешь.";
            else
                strInfo += "Все, бегут от тебя в ужасе. Скрип твоих мускул слышен за пару кварталов.";

            strInfo += Environment.NewLine + $"Ловкость({character.evasion}): ";

            if (character.evasion <= 15)
                strInfo += "У тебя достаточно ловкости, дабы увернутся от удара улитки, максимум комара.";
            else if (character.evasion <= 25)
                strInfo += "Соглашусь, ты что то да и можешь, но ты еще неповоротлив.";
            else
                strInfo += "Ловкость мангуста!";

            strInfo += Environment.NewLine + $"Броня({character.block}): "; ;

            if (character.block <= 10)
                strInfo += "Твоя защита оставляет желать лучшего, дуновение ветра и тебя снесет.";
            else if (character.block <= 15)
                strInfo += "Блокирую один удар, от второго лучше увернуться.";
            else
                strInfo += "Тебе не нужно двигаться, если тебя не могут пробить, верно ведь?";

            strInfo += Environment.NewLine + $"Удачка({character.fortune}): "; ;

            if (character.fortune <= 20)
                strInfo += "Удача? Что то ты о ней слышал, но никогда не видел.";
            else
                strInfo += "Она как бы есть, но не полностью.";

            lblStats.Text = strInfo;

        }

        /// <summary>
        /// Загрузка списков вещей
        /// </summary>
        private void LoadInfoEquipment()
        {
            lbCharacterEq.Items.Clear();
            lbInventoryEq.Items.Clear();

            foreach (Equipment eq in character.equipmentList)
                lbCharacterEq.Items.Add(eq.nameWithPrefix);

            foreach (Equipment eq in character.inventoryList)
                lbInventoryEq.Items.Add(eq.nameWithPrefix);
        }
        bool flagIzm = false;
        private void lbCharacterEq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flagIzm)
                return;
            flagIzm = true;
            if (((ListBox)(sender)).Tag.ToString() == "inventory")
                lbCharacterEq.ClearSelected();
            else if (((ListBox)(sender)).Tag.ToString() == "equipment")
                lbInventoryEq.ClearSelected();
            flagIzm = false;
        }

        Equipment eqNew;
        bool flagUpdate = false;
        private void btnEquip_Click(object sender, EventArgs e)
        {
            if (lbInventoryEq.SelectedItem == null)
                return;
            //Нахождение одеваемой вещи
            foreach (Equipment eq in character.inventoryList)
            {
                if (lbInventoryEq.SelectedItem.ToString() == eq.nameWithPrefix)
                {
                    eqNew = eq;
                    break;
                }
            }
            //Удаление одетых вещей типа новой вещи
            foreach (Equipment eq in character.equipmentList)
            {
                if (eq.typeEq == eqNew.typeEq)
                {
                    character.equipmentList.Remove(eq);
                    character.inventoryList.Add(eq);
                    break;
                }
            }
            character.equipmentList.Add(eqNew);
            character.inventoryList.Remove(eqNew);

            LoadInfoStats();
            LoadInfoEquipment();

            flagUpdate = true;
        }

        private void btnUnEquip_Click(object sender, EventArgs e)
        {
            if (lbCharacterEq.SelectedItem == null)
                return;
            //Нахождение снимаемой вещи
            foreach (Equipment eq in character.equipmentList)
            {
                if (lbCharacterEq.SelectedItem.ToString() == eq.nameWithPrefix)
                {
                    eqNew = eq;
                    break;
                }
            }
            character.equipmentList.Remove(eqNew);
            character.inventoryList.Add(eqNew);

            LoadInfoStats();
            LoadInfoEquipment();

            flagUpdate = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagUpdate)
                UpdateCharacterInfo();
        }

        private void UpdateCharacterInfo()
        {
            character.UpdateInfo();
        }

       
    }
}

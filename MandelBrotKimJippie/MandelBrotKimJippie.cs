using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "MandelBrotC";
scherm.BackColor = Color.LightPink;
scherm.ClientSize = new Size(600, 600);

// met een Bitmap kun je een plaatje opslaan in het geheugen
Bitmap plaatje = new Bitmap(400, 400);

// een Label kan ook gebruikt worden om een Bitmap te laten zien
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
// locatie van bitmap aanpassen
afbeelding.Location = new Point(10, 10);
afbeelding.Size = new Size(400, 400);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;

// label voor de invoer en tekstbox van x.
Label midden_x = new Label();
TextBox invoer_x = new TextBox();
scherm.Controls.Add(midden_x);
scherm.Controls.Add(invoer_x);
midden_x.Location = new Point(100, 450);
invoer_x.Location = new Point(180, 450);
midden_x.Text = "midden x:";
midden_x.Size = new Size(60, 20);
invoer_x.Size = new Size(140, 20);


// label voor de invoer en tekstbox van y.
Label midden_y = new Label();
TextBox invoer_y = new TextBox();
scherm.Controls.Add(midden_y);
scherm.Controls.Add(invoer_y);
midden_y.Location = new Point(100, 480);
invoer_y.Location = new Point(180, 480);
midden_y.Text = "midden y:";
midden_y.Size = new Size(60, 20);
invoer_y.Size = new Size(140, 20);

// label voor de invoer en tekstbox van schaal
Label schaal = new Label();
TextBox invoer_schaal = new TextBox();
scherm.Controls.Add(schaal);
scherm.Controls.Add(invoer_schaal);
schaal.Location = new Point(100, 510);
invoer_schaal.Location = new Point(180, 510);
schaal.Text = "schaal:";
schaal.Size = new Size(60, 20);
invoer_schaal.Size = new Size(140, 20);

// label voor de invoer en tekstbox van max aantal
Label maxaantal = new Label();
TextBox invoer_maxaantal = new TextBox();
scherm.Controls.Add(maxaantal);
scherm.Controls.Add(invoer_maxaantal);
maxaantal.Location = new Point(100, 540);
invoer_maxaantal.Location = new Point(180, 540);
maxaantal.Text = "max aantal:";
maxaantal.Size = new Size(60, 20);
invoer_maxaantal.Size = new Size(40, 20);

// button voor het genereren
Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(230, 540);
knop.Text = "Go!!!";
knop.Size = new Size(40, 25);

// button voor voorbeeld 1 
Button voorbeeld1 = new Button();
scherm.Controls.Add(voorbeeld1);
voorbeeld1.Location = new Point(270, 540);
voorbeeld1.Text = "Voorbeeld 1";
voorbeeld1.Size = new Size(90, 25);


// de code
// PointF newMandelPunt(float a, float b, float x, float y)
//{
//   return new PointF(a * a - b * b + x, 2 * a * b + y); 
//}


// maak een formule om elk pixel af te gaan en de waarde van a en b te berekenen 
/// we moeten een for loop voor x en y maken die dan in de mandel number functie komen 
/// dit doen we door de startpunt 0, max 400 (want 0-399 pixels is 400) en dan x++ zeg maar ;D
/// en dan ook voor y ofc
void pixelfinder() // wellicht nog een variabele maken voor de bitmap grote als het meer dan 400 is. 
{
    for (int pixel_x = 0; pixel_x<400; pixel_x++)
    {
        for (int pixel_y = 0; pixel_y<400; pixel_y++)
        {
            double newSchaal = double.Parse(invoer_schaal.Text);
            double newMiddenX = double.Parse(invoer_x.Text);
            double newMiddenY = double.Parse(invoer_y.Text);
            int maxCount2 = int.Parse(invoer_maxaantal.Text);

            double x = newMiddenX + ((pixel_x - (400 / 2)) * newSchaal); // calculates the x-coordinate belonging to the pixel
            double y = newMiddenY + (((400/2) - pixel_y) * newSchaal); // calculates the y-coordinate belonging to the pixel
         
            int mandelgetal = newMandelnumber(x, y);
             
            if (mandelgetal % 2 == 0)
                plaatje.SetPixel(pixel_x, pixel_y, Color.Black);
            else
                plaatje.SetPixel(pixel_x, pixel_y, Color.White);
           // else if (mandelgetal < 2) // for when the mandelgetal is infinite (aka pythagoras not larger than 2)
               //plaatje.SetPixel(pixel_x, pixel_y, Color.Black);
            
            
            // add an if else for when mandelgetal is infinite
        }
    }
}

int newMandelnumber(double x, double y)
{
    double a = 0;
    double b = 0;
    double a_tijdelijk = 0;
    int count = 0;
    int maxCount = int.Parse(invoer_maxaantal.Text);
    double pythagoras = 0;

    while (count < maxCount && pythagoras < 4)
    {
        a_tijdelijk = a * a - b * b + x;
        b = 2 * a * b + y;
        a = a_tijdelijk;
        pythagoras = a * a + b * b;
        count++; 

        // if (count > maxCount)
        //{
        //    count = maxCount;
         //   return count;
        //}
    }
    return count;
}

void genereer(object o, EventArgs e)
{
    pixelfinder();
    afbeelding.Invalidate();
}

void voorbeeld_one(object o, EventArgs e)
{
    invoer_schaal.Text = "0,01"; // sets the text in the textbox to a hardcoded value for an example.
    invoer_x.Text = "0";
    invoer_y.Text = "0";
    invoer_maxaantal.Text = "100";
    genereer(o, e);
}

void SchermClick(object sender, MouseEventArgs mea)
{
    double newSchaal = double.Parse(invoer_schaal.Text) * 0.80;
    int pixel_x = mea.X;
    int pixel_y = mea.Y;
    double newinvoer_x = ((pixel_x - (400 / 2)) * newSchaal); 
    double newinvoer_y = (((400 / 2) - pixel_y) * newSchaal);

    invoer_x.Text = newinvoer_x.ToString();
    invoer_y.Text = newinvoer_y.ToString();
    invoer_schaal.Text = newSchaal.ToString();
    

    afbeelding.Invalidate();
    pixelfinder();
}

// all click actions
afbeelding.MouseClick += SchermClick;
knop.Click += genereer;
voorbeeld1.Click += voorbeeld_one;

// DIT IS HET EINDE
Application.Run(scherm);




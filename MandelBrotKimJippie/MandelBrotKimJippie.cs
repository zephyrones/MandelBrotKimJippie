using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "MandelBrotC";
scherm.BackColor = Color.Gray;
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
// de code
PointF newMandelPunt(float a, float b, float x, float y)
{
    return new PointF(a * a - b * b + x, 2 * a * b + y); 
}

double Afstand(PointF punt_x, PointF punt_y)  // punt_x = mandelPunt and punt_y = beginpunt
{
    double p1X = Convert.ToDouble(punt_x.X);
    double p1Y = Convert.ToDouble(punt_x.Y);
    double p2X = Convert.ToDouble(punt_y.X);
    double p2Y = Convert.ToDouble(punt_y.Y);
    double dX = Math.Pow((p1X - p2X), 2);
    double dY = Math.Pow((p1Y - p2Y), 2);
    return Math.Sqrt(dX + dY);
}
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
            float newSchaal = float.Parse(invoer_schaal.Text);
            float x = pixel_x * newSchaal - 2; // calculates the x-coordinate belonging to the pixel
            float y = pixel_y * -newSchaal + 2; // calculates the y-coordinate belonging to the pixel
            int maxCount2 = int.Parse(invoer_maxaantal.Text);
            int mandelgetal = newMandelnumber(x, y);
             
            if (mandelgetal % 2 == 0)
                plaatje.SetPixel(pixel_x, pixel_y, Color.Black);
            else if (mandelgetal % 2 != 0)
                plaatje.SetPixel(pixel_x, pixel_y, Color.White);
            else if (mandelgetal == maxCount2) // for when the mandelgetal is infinite (aka pythagoras not larger than 2)
                plaatje.SetPixel(pixel_x, pixel_y, Color.Black);
            
            
            // add an if else for when mandelgetal is infinite
        }
    }
}

int newMandelnumber(float x, float y)
{
    float a = 0f;
    float b = 0f;
    int count = 0;
    int maxCount = int.Parse(invoer_maxaantal.Text);
    PointF beginpunt = new PointF(x, y);
    double pythagoras = 0;

    while (count < maxCount && pythagoras < 2)
    {
        PointF mandelPunt = newMandelPunt(a, b, x, y);
        pythagoras = Afstand(mandelPunt, beginpunt);
        a = mandelPunt.X;
        b = mandelPunt.Y;
        count++; 
    }
    return count;
}

void genereer(object o, EventArgs e)
{
    pixelfinder();
    afbeelding.Invalidate();
}

knop.Click += genereer;



// DIT IS HET EINDE
Application.Run(scherm);




// Jip Heimeriks and Kim Nieuwets

using System;
using System.Drawing;
using System.Windows.Forms;


Form scherm = new Form();
scherm.Text = "Mandelbrot Generator!";
scherm.BackColor = Color.LightPink;
scherm.ClientSize = new Size(650, 650);

// using a bitmap to save an image in the memory
Bitmap plaatje = new Bitmap(400, 400);

// use a label to show a bitmap on screen
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(10, 10);
afbeelding.Size = new Size(400, 400);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;

// label and textbox for x-value.
Label midden_x = new Label();
TextBox invoer_x = new TextBox();
scherm.Controls.Add(midden_x);
scherm.Controls.Add(invoer_x);
midden_x.Location = new Point(10, 420);
invoer_x.Location = new Point(90, 420);
midden_x.Text = "Midden x:";
midden_x.Size = new Size(60, 20);
invoer_x.Size = new Size(140, 20);


// label and textbox for y-value.
Label midden_y = new Label();
TextBox invoer_y = new TextBox();
scherm.Controls.Add(midden_y);
scherm.Controls.Add(invoer_y);
midden_y.Location = new Point(10, 450);
invoer_y.Location = new Point(90, 450);
midden_y.Text = "Midden y:";
midden_y.Size = new Size(60, 20);
invoer_y.Size = new Size(140, 20);

// label and textbox for schaal.
Label schaal = new Label();
TextBox invoer_schaal = new TextBox();
scherm.Controls.Add(schaal);
scherm.Controls.Add(invoer_schaal);
schaal.Location = new Point(10, 480);
invoer_schaal.Location = new Point(90, 480);
schaal.Text = "Schaal:";
schaal.Size = new Size(60, 20);
invoer_schaal.Size = new Size(140, 20);

// label en textbox for max aantal.
Label maxaantal = new Label();
TextBox invoer_maxaantal = new TextBox();
scherm.Controls.Add(maxaantal);
scherm.Controls.Add(invoer_maxaantal);
maxaantal.Location = new Point(10, 510);
invoer_maxaantal.Location = new Point(90, 510);
maxaantal.Text = "Max aantal:";
maxaantal.Size = new Size(75, 20);
invoer_maxaantal.Size = new Size(40, 20);

// Label voor Trackbars
Label trackbar = new Label();
scherm.Controls.Add(trackbar);
trackbar.Location = new Point(10, 540);
trackbar.Size = new Size(120, 20);
trackbar.Text = "Waarde kleur groen: ";

Label trackbar2 = new Label();
scherm.Controls.Add(trackbar2);
trackbar2.Location = new Point(10, 580);
trackbar2.Size = new Size(120, 20);
trackbar2.Text = "Waarde kleur blauw: ";


// button voor het genereren
Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(140, 510);
knop.Text = "Genereer!";
knop.Size = new Size(90, 25);

// button voor voorbeeld 1 
Button voorbeeld1 = new Button();
scherm.Controls.Add(voorbeeld1);
voorbeeld1.Location = new Point(250, 420);
voorbeeld1.Text = "Voorbeeld 1";
voorbeeld1.Size = new Size(90, 25);

// button voor voorbeeld 2
Button voorbeeld2 = new Button();
scherm.Controls.Add(voorbeeld2);
voorbeeld2.Location = new Point(250, 450);
voorbeeld2.Text = "Voorbeeld 2";
voorbeeld2.Size = new Size(90, 25);

// button voor voorbeeld 3
Button voorbeeld3 = new Button();
scherm.Controls.Add(voorbeeld3);
voorbeeld3.Location = new Point(250, 480);
voorbeeld3.Text = "Voorbeeld 3";
voorbeeld3.Size = new Size(90, 25);

// button voor voorbeeld 4
Button voorbeeld4 = new Button();
scherm.Controls.Add(voorbeeld4);
voorbeeld4.Location = new Point(250, 510);
voorbeeld4.Text = "Voorbeeld 4";
voorbeeld4.Size = new Size(90, 25);

// trackbars for color
TrackBar schuif = new TrackBar(); // connected to green value
scherm.Controls.Add(schuif);
schuif.Location = new Point(130, 540);
schuif.Size = new Size(200, 20);
schuif.Minimum = 0;
schuif.Maximum = 255;
schuif.Orientation= Orientation.Horizontal;

TrackBar schuif2 = new TrackBar(); // connected to blue value
scherm.Controls.Add(schuif2);
schuif2.Location = new Point(130, 580);
schuif2.Size = new Size(200, 20);
schuif2.Minimum = 0;
schuif2.Maximum = 85;
schuif2.Orientation = Orientation.Horizontal;



// startvalues voor de textboxes
invoer_schaal.Text = "0,01"; 
invoer_x.Text = "0";
invoer_y.Text = "0";
invoer_maxaantal.Text = "100";
pixelfinder();

void pixelfinder()  
{
    for (int pixel_x = 0; pixel_x<400; pixel_x++) // runs through all x and y pixels to calculate the x and y coords.
    {
        for (int pixel_y = 0; pixel_y<400; pixel_y++)
        {

            double newSchaal = double.Parse(invoer_schaal.Text);
            double newMiddenX = double.Parse(invoer_x.Text);
            double newMiddenY = double.Parse(invoer_y.Text);
            int maxCount = int.Parse(invoer_maxaantal.Text);

            double x = coordinaatx(newMiddenX, pixel_x, newSchaal);
            double y = coordinaaty(newMiddenY, pixel_y, newSchaal);
           // double x = newMiddenX + ((pixel_x - (400 / 2)) * newSchaal); // calculates the x-coordinate belonging to the pixel
           // double y = newMiddenY + (((400/2) - pixel_y) * newSchaal); // calculates the y-coordinate belonging to the pixel
         
            int mandelgetal = newMandelnumber(x, y);

            int rood = (255 * mandelgetal / maxCount);
            int groen = schuif.Value;

            if (mandelgetal % 2 == 0)
            { // the starting point for each if-else statement is different and falls between 0-255.
              // the trackbar has a min = 0 and max = 85. this is to determine the blue value based on 
              // the mandelnumber.
              
                int blauw = 85 + schuif2.Value;
                plaatje.SetPixel(pixel_x, pixel_y, Color.FromArgb(rood, groen, blauw));
            }
            else if (mandelgetal % 3 == 0)
            {
               
                int blauw = 170 + schuif2.Value;
                plaatje.SetPixel(pixel_x, pixel_y, Color.FromArgb(rood, groen, blauw));
            }
            else
            {
               
                int blauw = schuif2.Value;
                plaatje.SetPixel(pixel_x, pixel_y, Color.FromArgb(rood, groen, blauw));
            }
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

        if (count > maxCount)
        {
        count = maxCount;
        return count;
        }
    }
    return count;
}

// functions that calculate the x and y coordinate based on the pixel.
double coordinaatx(double newMiddenX, double pixel_x, double newSchaal)
{
    double x = newMiddenX + ((pixel_x - (400 / 2)) * newSchaal);
    return x;
}

double coordinaaty(double newMiddenY, double pixel_y, double newSchaal)
{
    double y = newMiddenY + (((400 / 2) - pixel_y) * newSchaal);
    return y;
}

// function that generates the image
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

void voorbeeld_two(object o, EventArgs e)
{
    invoer_x.Text = "-0,108625"; // sets the text in the textbox to a hardcoded value for an example.
    invoer_y.Text = "0,9014428";
    invoer_schaal.Text = "3,8147E-8";
    invoer_maxaantal.Text = "400";
    genereer(o, e);
}


void voorbeeld_three(object o, EventArgs e)
{
    invoer_x.Text = "-0,16745422105304897"; // sets the text in the textbox to a hardcoded value for an example.
    invoer_y.Text = "1,0412260058801621";
    invoer_schaal.Text = "5,8207660913467E-11";
    invoer_maxaantal.Text = "6000";
    genereer(o, e);
}

void voorbeeld_four(object o, EventArgs e)
{
    invoer_x.Text = "-0,1674541869683058"; // sets the text in the textbox to a hardcoded value for an example.
    invoer_y.Text = "1,0412260396976252";
    invoer_schaal.Text = "1,1879114472136123E-10";
    invoer_maxaantal.Text = "1500";
    genereer(o, e);
}
void SchermClick(object sender, MouseEventArgs mea)
{
    if (mea.Button == MouseButtons.Left)
    {
        double newSchaal = double.Parse(invoer_schaal.Text) * 0.7;
        double middenX = double.Parse(invoer_x.Text);
        double middenY = double.Parse(invoer_y.Text);

        int pixel_x = mea.X;
        int pixel_y = mea.Y;

        double newinvoer_x = coordinaatx(middenX, pixel_x, newSchaal);
        double newinvoer_y = coordinaaty(middenY, pixel_y, newSchaal);      

        invoer_x.Text = newinvoer_x.ToString();
        invoer_y.Text = newinvoer_y.ToString();
        invoer_schaal.Text = newSchaal.ToString();

        afbeelding.Invalidate();
        pixelfinder();
    }
    else if (mea.Button == MouseButtons.Right)
    {
        double newSchaal = double.Parse(invoer_schaal.Text) / 0.7;
        double middenX = double.Parse(invoer_x.Text);
        double middenY = double.Parse(invoer_y.Text);

        int pixel_x = mea.X;
        int pixel_y = mea.Y;
        double newinvoer_x = coordinaatx(middenX, pixel_x, newSchaal);
        double newinvoer_y = coordinaaty(middenY, pixel_y, newSchaal);
        double temp_newinvoer_x = newinvoer_x;
        double temp_newinvoer_y = newinvoer_y;

        invoer_x.Text = temp_newinvoer_x.ToString();
        invoer_y.Text = temp_newinvoer_y.ToString();
        invoer_schaal.Text = newSchaal.ToString();

        afbeelding.Invalidate();
        pixelfinder();
    }
}

void VeranderSchuif(object o, EventArgs ea)
{
    pixelfinder();
    afbeelding.Invalidate();
}

void VeranderSchuif2(object o, EventArgs ea)
{
    pixelfinder();
    afbeelding.Invalidate();
}

// all actions
afbeelding.MouseClick += SchermClick;
knop.Click += genereer;
voorbeeld1.Click += voorbeeld_one;
voorbeeld2.Click += voorbeeld_two;
voorbeeld3.Click += voorbeeld_three;
voorbeeld4.Click += voorbeeld_four;
schuif.Scroll += VeranderSchuif;
schuif2.Scroll += VeranderSchuif2;

Application.Run(scherm);




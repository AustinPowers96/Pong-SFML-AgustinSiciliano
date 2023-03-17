using pong_sfml;
using pong_sfml.classes.ui;
using pong_sfml.States;
using SFML.Graphics;
using SFML.System;
using System;



public class MainMenuState : LoopState
{
    private Font titleFont;
    private Text titleText;
    private Text titleText1;
    private Button playButton;
    private Button controlsButton;
    private Button creditsButton;
    private Button quitButton;
    public event Action OnPlayPressed;
    public event Action OnControlsPressed;
    public event Action OnCreditsPressed;
    public event Action OnQuitPressed;
    

    public MainMenuState(RenderWindow window) : base(window)
    {
        
    }

    private void OnPressPlay() => OnPlayPressed?.Invoke();
    private void OnPressControls() => OnControlsPressed?.Invoke();
    private void OnPressCredits() => OnCreditsPressed?.Invoke();
    private void OnPressQuit() => OnQuitPressed?.Invoke();

    protected override void Start()
    {
        string fontFilePath = "Assets/Fonts/Hyperspace.ttf";
        string buttonImageFilePath = "Assets/Images/Button.png";
        float firstButtonOffset = 70f;
        float buttonSpacing = 120f;
        Global.sfx["songMainMenu"].play();
       

        titleFont = new Font(fontFilePath);
        titleText = new Text("Pong", titleFont);
        titleText1 = new Text("The first to reach 10 points wins!", titleFont);


        titleText.CharacterSize = 160;
        titleText.FillColor = Color.White;
        titleText.OutlineColor = Color.Blue;
        titleText.OutlineThickness = 5f;

        titleText1.CharacterSize = 40;
        titleText1.FillColor = Color.Blue;
        titleText1.OutlineColor = Color.Cyan;
        titleText1.OutlineThickness = 1f;

        FloatRect titleRect = titleText.GetLocalBounds();

        titleText.Origin = new Vector2f(titleRect.Width / 2f, titleRect.Height / 0.4f);
        titleText.Position = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 1.8f - buttonSpacing);

        titleText1.Origin = new Vector2f(titleRect.Width / 2f, titleRect.Height / 0.6f);
        titleText1.Position = new Vector2f(Global.ScreenSize.X / 2.8f, Global.ScreenSize.Y / 1.5f - buttonSpacing);

        Vector2f playButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 3.5f + firstButtonOffset);
        Vector2f controlsButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2.2f + firstButtonOffset);
        Vector2f creditsButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 1.6f + firstButtonOffset);
        Vector2f quitButtonPosition = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 1.6f + firstButtonOffset + buttonSpacing);

        playButton = new Button(this.Window, buttonImageFilePath, fontFilePath, playButtonPosition, "Play");
        controlsButton = new Button(this.Window, buttonImageFilePath, fontFilePath, controlsButtonPosition, "Controls");
        creditsButton = new Button(this.Window, buttonImageFilePath, fontFilePath, creditsButtonPosition, "Credits");
        quitButton = new Button(this.Window, buttonImageFilePath, fontFilePath, quitButtonPosition, "Quit");

        playButton.SetColor(Color.Green);
        controlsButton.SetColor(Color.Blue);
        creditsButton.SetColor(Color.Magenta);
        quitButton.SetColor(Color.Red);

        playButton.FormatText(fillColor: Color.Green, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);
        controlsButton.FormatText(fillColor: Color.Blue, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);
        creditsButton.FormatText(fillColor: Color.Magenta, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);
        quitButton.FormatText(fillColor: Color.Red, outlineColor: Color.White, size: 30, outline: true, outlineThickness: 2f);

        View view = new View(this.Window.GetView());

        view.Center = new Vector2f(Global.ScreenSize.X / 2f, Global.ScreenSize.Y / 2f);

        this.Window.SetView(view);

        playButton.OnPressed += OnPressPlay;
        controlsButton.OnPressed += OnPressControls;
        creditsButton.OnPressed += OnPressCredits;
        quitButton.OnPressed += OnPressQuit;
        this.Window.Closed += OnCloseWindow;
    }

    protected override void Update(float deltaTime)
    {
       
    }

    protected override void Draw()
    {
        this.Window.Draw(titleText);
        this.Window.Draw(titleText1);
        controlsButton.Draw();
        creditsButton.Draw();
        playButton.Draw();
        quitButton.Draw();
    }

    protected override void Finish()
    {
        playButton.OnPressed -= OnPressPlay;
        controlsButton.OnPressed -= OnPressControls;
        creditsButton.OnPressed -= OnPressCredits;
        quitButton.OnPressed -= OnPressQuit;
        this.Window.Closed -= OnCloseWindow;
        Global.sfx["songMainMenu"].stop();

    }

}


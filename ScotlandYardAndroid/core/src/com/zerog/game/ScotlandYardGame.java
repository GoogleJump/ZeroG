package com.zerog.game;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.Pixmap;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.Pixmap.Format;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;

public class ScotlandYardGame extends Game {
	SpriteBatch batch;
	BitmapFont font;
	Skin skin;
	
	MainMenuScreen mainMenuScreen;
	SettingsScreen settingsScreen;
	
	@Override
	public void create () {
		this.batch = new SpriteBatch();
		this.font = new BitmapFont();
		this.skin = new Skin();
		
		Pixmap pixmap = new Pixmap(100, 100, Format.RGBA8888);
        pixmap.setColor(Color.GREEN);
        pixmap.fill();
        skin.add("button", new Texture(pixmap));
        // Store the default libgdx font under the name "default".
        BitmapFont bfont = new BitmapFont();
        bfont.scale(1);
        skin.add("default",bfont);
        
        final TextButtonStyle textButtonStyle = createButtonStyle(skin);
		
        MainMenuScreen mainMenuScreen = new MainMenuScreen(this, textButtonStyle, settingsScreen);
		SettingsScreen settingsScreen = new SettingsScreen(this, textButtonStyle, mainMenuScreen);
        this.setScreen(mainMenuScreen);
	}

	@Override
	public void render () {
		super.render();
	}
	
	public void dispose() {
        batch.dispose();
        font.dispose();
    }
	
	private TextButtonStyle createButtonStyle(Skin skin) {
        TextButtonStyle textButtonStyle = new TextButtonStyle();
        textButtonStyle.up = skin.newDrawable("button", Color.DARK_GRAY);
        textButtonStyle.down = skin.newDrawable("button", Color.DARK_GRAY);
        textButtonStyle.checked = skin.newDrawable("button", Color.BLUE);
        textButtonStyle.over = skin.newDrawable("button", Color.LIGHT_GRAY);
        textButtonStyle.font = skin.getFont("default");
        skin.add("default", textButtonStyle);	
        
        return textButtonStyle;
	}
}
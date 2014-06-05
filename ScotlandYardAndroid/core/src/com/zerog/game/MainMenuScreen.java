package com.zerog.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;
import com.badlogic.gdx.scenes.scene2d.Actor;


public class MainMenuScreen implements Screen {

	final ScotlandYardGame game;
	Stage stage;
	
	final int playBtnXPos = 200;
	final int playBtnYPos = 200;
	
	final int settingsBtnXPos = 500;
	final int settingsBtnYPos = 200;
	
	TextButton playBtn;
	TextButton settingsBtn;
	TextButtonStyle textButtonStyle;
	
	public MainMenuScreen(final ScotlandYardGame game, final TextButtonStyle textButtonStyle){
		this.game = game;
		this.stage = new Stage();
		this.textButtonStyle = textButtonStyle;
		create();
	}
	
	public void create(){
		System.out.println("create in mainemnu called");
        playBtn = new TextButton("Play", textButtonStyle);
        playBtn.setPosition(playBtnXPos, playBtnYPos);
        
        settingsBtn = new TextButton("Settings", textButtonStyle);
        settingsBtn.setPosition(settingsBtnXPos, settingsBtnYPos);
        
        stage.addActor(playBtn);
        stage.addActor(settingsBtn);
        
        playBtn.addListener(new ChangeListener() {
            public void changed (ChangeEvent event, Actor actor) {
                //game.setScreen( new GameScreen());
            }
        });
        
        settingsBtn.addListener(new ChangeListener() {
            public void changed (ChangeEvent event, Actor actor) {
                game.setScreen(game.settingsScreen);
            }
        });
        
	}
	

	@Override
	public void render(float delta) {
		Gdx.gl.glClearColor(0, 0, 0.2f, 1);
		Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

		stage.act(Math.min(Gdx.graphics.getDeltaTime(), 1 / 30f));
        stage.draw();
	}

	@Override
	public void resize(int width, int height) {
		// TODO Auto-generated method stub
	}

	@Override
	public void show() {
		Gdx.input.setInputProcessor(stage);
		System.out.println("show in mainmenu called");
		settingsBtn.setChecked(false);
		// TODO Auto-generated method stub
	}

	@Override
	public void hide() {
		// TODO Auto-generated method stub
	}

	@Override
	public void pause() {
		// TODO Auto-generated method stub
	}

	@Override
	public void resume() {
		// TODO Auto-generated method stub
	}

	@Override
	public void dispose() {
		// TODO dispose objects here, call in hide()?
	}

}

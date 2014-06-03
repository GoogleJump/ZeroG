package com.zerog.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

public class SettingsScreen implements Screen {
	
	final ScotlandYardGame game;
	final TextButtonStyle textButtonStyle;
	Stage stage;
	
	final int numOfDetectivesSBXPos = 200;
	final int numOfDetectivesSBYPos = 200;
	
	final int settingsBtnXPos = 500;
	final int settingsBtnYPos = 200;

	public SettingsScreen(final ScotlandYardGame game, TextButtonStyle textButtonStyle) {
		this.game = game;
		this.stage = new Stage();
		this.textButtonStyle = textButtonStyle;
		create();
	}


	private void create() {
		Gdx.input.setInputProcessor(stage);
        final TextButton mainMenuBtn = new TextButton("Main Menu", textButtonStyle);
        mainMenuBtn.setPosition(settingsBtnXPos, settingsBtnYPos);
        
        //Other buttons here
        
        stage.addActor(mainMenuBtn);
        
        mainMenuBtn.addListener(new ChangeListener() {
            public void changed (ChangeEvent event, Actor actor) {
                game.setScreen(new MainMenuScreen(game,textButtonStyle));
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

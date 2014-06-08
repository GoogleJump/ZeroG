package com.zerog.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;

public class GameplayScreen implements Screen {
	
	final ScotlandYardGame game;
	final TextButtonStyle textButtonStyle;
	final Stage stage;
	
	Texture board;
	Camera boardCamera;
	Batch stageSpriteBatch; 

	
	public GameplayScreen(final ScotlandYardGame game, TextButtonStyle textButtonStyle) {
		this.game = game;
		this.stage = new Stage();
		this.stageSpriteBatch = stage.getSpriteBatch();
		this.boardCamera = new OrthographicCamera();
		this.textButtonStyle = textButtonStyle;
		create();
	}

	private void create() {
		board = new Texture(Gdx.files.internal("map.jpg"));
        
	}
	
	@Override
	public void render(float delta) {
		stageSpriteBatch.begin();
		stageSpriteBatch.draw(board, 0, 0);
		stageSpriteBatch.end();
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
		// TODO Auto-generated method stub

	}

}

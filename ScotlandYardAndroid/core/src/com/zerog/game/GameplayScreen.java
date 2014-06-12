package com.zerog.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.InputMultiplexer;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.input.GestureDetector;
import com.badlogic.gdx.input.GestureDetector.GestureListener;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Image;
import com.badlogic.gdx.scenes.scene2d.ui.ScrollPane;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;

public class GameplayScreen implements Screen {
	
	final ScotlandYardGame game;
	final TextButtonStyle textButtonStyle;
	final Stage stage;
	
	Texture boardTexture;
	Image boardImage;
	Camera camera;
	Batch stageSpriteBatch; 
	
	
	public GameplayScreen(final ScotlandYardGame game, TextButtonStyle textButtonStyle) {
		this.game = game;
		this.textButtonStyle = textButtonStyle;
		
		this.stage = new Stage();
		this.stageSpriteBatch = stage.getSpriteBatch();
		this.camera = stage.getCamera();
		create();
	}

	private void create() {
		boardTexture = new Texture(Gdx.files.internal("map.jpg"));
		boardImage = new Image(boardTexture);
		ScrollPane scrollPane = new ScrollPane(boardImage);
		scrollPane.setOverscroll(false, false);
		scrollPane.setSize(800, 480);
		stage.addActor(scrollPane);
	}
	
	@Override
	public void render(float delta) {
		//stageSpriteBatch.begin();
		//stageSpriteBatch.draw(board, 0, 0);
		//stageSpriteBatch.end();
		stage.act(delta);
		stage.draw();
	}

	@Override
	public void resize(int width, int height) {
		// TODO Auto-generated method stub

	}

	@Override
	public void show() {
		System.out.println("show gameplay");
		Gdx.input.setInputProcessor(stage);
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

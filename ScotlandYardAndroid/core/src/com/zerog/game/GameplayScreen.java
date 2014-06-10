package com.zerog.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.InputMultiplexer;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.input.GestureDetector;
import com.badlogic.gdx.input.GestureDetector.GestureListener;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton.TextButtonStyle;

public class GameplayScreen implements Screen, GestureListener {
	
	final ScotlandYardGame game;
	final TextButtonStyle textButtonStyle;
	final Stage stage;
	
	Texture board;
	Camera camera;
	Batch stageSpriteBatch; 
	
	GestureDetector gestureDetector;
	InputMultiplexer inputMultiplexer;

	
	public GameplayScreen(final ScotlandYardGame game, TextButtonStyle textButtonStyle) {
		this.game = game;
		this.stage = new Stage();
		this.stageSpriteBatch = stage.getSpriteBatch();
		this.camera = stage.getCamera();
		this.textButtonStyle = textButtonStyle;
		this.gestureDetector = new GestureDetector(this);
		inputMultiplexer = new InputMultiplexer(gestureDetector, stage);
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
		Gdx.input.setInputProcessor(inputMultiplexer);
		System.out.println("show gameplay");
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

	@Override
	public boolean touchDown(float x, float y, int pointer, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean tap(float x, float y, int count, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean longPress(float x, float y) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean fling(float velocityX, float velocityY, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean pan(float x, float y, float deltaX, float deltaY) {
		System.out.println("pan called");
		System.out.println(deltaX);
		int deltaZ = 0;
		 camera.translate(deltaX, deltaY, deltaZ);
		 camera.update();
		 
		return false;
	}

	@Override
	public boolean panStop(float x, float y, int pointer, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean zoom(float initialDistance, float distance) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean pinch(Vector2 initialPointer1, Vector2 initialPointer2,
			Vector2 pointer1, Vector2 pointer2) {
		// TODO Auto-generated method stub
		return false;
	}

}

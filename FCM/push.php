<?php

/**
 * @author Ravi Tamada
 * @link URL Tutorial link
 */
class Push {

    // push message title
    private $title;
    private $text;
    private $image;
    // push message payload
    private $data;
    // flag indicating whether to show the push
    // notification or not
    // this flag will be useful when perform some opertation
    // in background when push is recevied
    private $is_background;

    function __construct() {
        
    }

    public function setTitle($title) {
        $this->title = $title;
    }

    public function setText($text) {
        $this->text = $text;
    }

    public function setImage($imageUrl) {
        $this->image = $imageUrl;
    }

    public function setPayload($data) {
        $this->data = $data;
    }

    public function setIsBackground($is_background) {
        $this->is_background = $is_background;
    }

    public function getPush() {
        $res = array();
        $res['Data']['title'] = $this->title;
        $res['Data']['is_background'] = $this->is_background;
        $res['Data']['text'] = $this->text;
        $res['Data']['image'] = $this->image;
        $res['Data']['payload'] = $this->data;
		$res['Data']['vibrate']	= 1;
		$res['Data']['lights']	= 1;
	    $res['Data']['sound']	=1;
		 $res['Data']['id'] = 1;
	    $res['Data']['largeIcon'] = 'large_icon';
	    $res['Data']['smallIcon']= 'small_icon';
		
        $res['Data']['timestamp'] = date('Y-m-d G:i:s');
        return $res;
    }

}

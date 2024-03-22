import React from 'react';

const YoutubeItem = (props) => {
    return (
        <div className="youtube-item">
        <div  className="youtube-image">
            <img src={props.image}/>
        </div>
        <div className="youtube-content">
            <h3>{props.title || 'Nothing Special!!!'}</h3>
            
        </div>
      </div>
  
    );
};

export default YoutubeItem;
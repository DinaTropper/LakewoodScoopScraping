import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Home = () => {
    const [posts, setPosts] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const { title, url, image, blurb, amountOfComments } = posts;

    useEffect(() => {
        const getData = async () => {
            const { data } = await axios.get('/api/scraper/scraper');
            setPosts(data);
        }
        getData();
        setIsLoading(false);
    }, []);
    return (
        <div className="container" style={{ marginTop: 80 }}>

            {!!posts.length &&
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Blurb</th>
                            <th>Amount of Comments</th>
                        </tr>
                    </thead>
                    <tbody>
                        {posts.map(p => (
                            <tr key={p.title}>
                                <td><img src={p.image} style={{ height: 150 }} alt={p.title} className="img-thumbnail" /></td>
                                <td>
                                    <a href={p.url} target='_blank'>
                                        {p.title}
                                    </a>
                                </td>
                                <td>
                                    <span>{p.blurb}</span>
                                </td>
                                <td>{p.amountOfComments}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            }
        </div>
    );
};

export default Home;
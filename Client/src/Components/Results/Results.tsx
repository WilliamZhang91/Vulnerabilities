interface Props {
    results: string
}

const Results: React.FC<Props> = ({ results }): JSX.Element => {
    
    const formattedResults = results.replace(/(?:\r\n|\r|\n)/g, '<br>');

    return (
        <section>
            <p dangerouslySetInnerHTML={{ __html: formattedResults }} />
        </section>
    );
};
export default Results; 